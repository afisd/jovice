﻿using Aphysoft.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jovice
{
    internal class Assumption
    {
        private string intent;

        public string Intent
        {
            get { return intent; }
        }

        private bool asking;

        public bool Asking
        {
            get { return asking; }
        }

        private bool mentionMyName;

        public bool MentionMyName
        {
            get { return mentionMyName; }
            set { mentionMyName = value; }
        }

        public Assumption(string intent, bool asking, bool mentionMyName)
        {
            this.intent = intent;
            this.asking = asking;
            this.mentionMyName = mentionMyName;
        }
    }
    
    internal class Intent
    {
        private static string name = null;

        private List<Assumption> assumptions = new List<Assumption>();

        public Assumption[] Assumptions
        {
            get { return assumptions.ToArray(); }
        }

        public Intent()
        {

        }
        
        private void Add(Assumption entity)
        {
            assumptions.Add(entity);
        }

        private static Regex urlRegex = new Regex(@"(((http|ftp|https):\/\/)*[\w\-_]+(\.(com|net|org|edu|ac|co|go|id|us|tk|sg|au|center|tv|info|xyz|gov))+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

        private static char[] messageSeparators = { ' ', '.', ',', '?', '!' };
        private static char[] tokenTrims = { '~', '`', '@', '#', '$', '^', '*' };

        private static string[] constraintPriority = { "SW" };

        private static Dictionary<string, List<Tuple<string, string>>> intentReferences = new Dictionary<string, List<Tuple<string, string>>>();

        public static void Init(string name)
        {
            Intent.name = name;

            Result2 result;
            Database2 share = Web.Database;

            intentReferences.Clear();

            // update nlw yg blum ada double metaphonenya
            result = share.Query("select * from LanguageIntent where LI_Word is not null and LI_DM is null");

            if (result.Count > 0)
            {
                Batch batch = share.Batch();
                batch.Begin();
                foreach (Row2 row in result)
                {
                    string word = row["LI_Word"].ToString();
                    DoubleMetaphone dm = new DoubleMetaphone(word);
                    string dmw = string.Format("{0}-{1}", dm.PrimaryKey, dm.AlternateKey);

                    Update update = share.Update("LanguageIntent");
                    update.Set("LI_DM", dmw);
                    update.Where("LI_ID", row["LI_ID"].ToLong());
                    batch.Add(update);
                }
                batch.Commit();
            }

            List<string> intents = share.QueryList("select distinct LI_Intent from LanguageIntent", "LI_Intent");

            foreach (string intent in intents)
            {
                Column2 cmax = share.Scalar("select max(LI_Position) from LanguageIntent where LI_Intent = {0}", intent);
                int max = cmax.ToInt() + 1;

                List<Tuple<string, string>>[] lists = new List<Tuple<string, string>>[max];
                for (int i = 0; i < max; i++) lists[i] = new List<Tuple<string, string>>();

                result = share.Query("select LI_Position, LI_Constraint, LI_DM from LanguageIntent where LI_Intent = {0}", intent);

                foreach (Row2 row in result)
                {
                    int pos = row["LI_Position"].ToInt();
                    lists[pos].Add(new Tuple<string, string>(row["LI_DM"].ToString(), row["LI_Constraint"].ToString()));
                }

                IntentWordCombination(lists, intent, null, new StringBuilder(), 0);
            }

            foreach (KeyValuePair<string, List<Tuple<string, string>>> pair in intentReferences)
            {
                pair.Value.Sort(delegate (Tuple<string, string> a, Tuple<string, string> b)
                {
                    string ae = a.Item2;
                    string be = b.Item2;

                    if (ae == be) return 0;
                    else if (ae != null && be == null) return -1;
                    else if (ae == null && be != null) return 1;
                    else
                    {
                        int ac = Array.IndexOf(constraintPriority, ae);
                        int bc = Array.IndexOf(constraintPriority, be);

                        if (ac < bc) return 1;
                        else if (ac > bc) return -1;
                        else return 0;
                    }

                });
            }

        }

        private static void IntentWordCombination(List<Tuple<string, string>>[] lists, string intent, string constraint, StringBuilder sb, int index)
        {
            if (index < lists.Length)
            {
                List<Tuple<string, string>> list = lists[index];

                if (index > 0) sb.Append(" ");

                foreach (Tuple<string, string> entry in list)
                {
                    StringBuilder sbc = new StringBuilder(sb.ToString());

                    if (index == 0) constraint = entry.Item2;

                    IntentWordCombination(lists, intent, constraint, sbc.Append(entry.Item1), index + 1);
                }
            }
            else
            {
                string dmkey = sb.ToString().Trim();
                if (!intentReferences.ContainsKey(dmkey))
                {
                    List<Tuple<string, string>> entryList = new List<Tuple<string, string>>();
                    intentReferences.Add(dmkey, entryList);
                }
                intentReferences[dmkey].Add(new Tuple<string, string>(intent, constraint));
            }
        }
        
        public static Intent Parse(string message)
        {
            if (message.Length > 50) return null;

            string messageLower = message.ToLower().Trim();

            string messageOneSpace = string.Join(" ", messageLower.Split(StringSplitTypes.Space, StringSplitOptions.RemoveEmptyEntries));

            List<string> tokens = new List<string>();
            List<string> dmTokens = new List<string>();

            int startIndex = 0;
            foreach (string token in messageLower.Split(messageSeparators, StringSplitOptions.RemoveEmptyEntries))
            {                
                if (token == name)
                {
                    tokens.Add("{MENTIONMYNAME}");
                    continue;
                }

                int tokenIndex = messageOneSpace.IndexOf(token, startIndex);
                startIndex = tokenIndex + token.Length;

                if (tokenIndex >= 2)
                {
                    if (messageOneSpace[tokenIndex - 2] == '?') tokens.Add("{QUESTIONMARK}");
                }
                                
                string ntoken = token;
                if (ntoken.Length > 2 && !char.IsDigit(ntoken[0]) && ntoken.EndsWith("2"))
                    ntoken = string.Format("{0}-{0}", ntoken);
                ntoken = ntoken.Trim(tokenTrims);

                tokens.Add(token);
            }

            if (messageOneSpace.IndexOf("?", startIndex) > -1) tokens.Add("{QUESTIONMARK}");
            
            if (tokens.Count > 0)
            {
                foreach (string token in tokens)
                {
                    if (token.StartsWith("{") && token.EndsWith("}")) dmTokens.Add(token);
                    else
                    {
                        DoubleMetaphone dm = new DoubleMetaphone(token);
                        string dmToken = string.Format("{0}-{1}", dm.PrimaryKey, dm.AlternateKey);
                        dmTokens.Add(dmToken);
                    }
                }                

                int checkPair = 4;
                if (dmTokens.Count < checkPair) checkPair = dmTokens.Count;

                // 0 1 2 3 4 5 6 7
                // 3 4 5
                // i = 3, checkPair = 3
                // 0 1 2 6 7

                Intent intent = new Intent();
                
                while (checkPair > 0)
                {
                    int until = dmTokens.Count - checkPair;

                    for (int i = 0; i <= until; i++)
                    {
                        string s = string.Join(" ", dmTokens.ToArray(), i, checkPair).Trim();

                        if (s != string.Empty)
                        {
                            if (intentReferences.TryGetValue(s, out List<Tuple<string, string>> references))
                            {
                                foreach (Tuple<string, string> reference in references)
                                {
                                    string name = reference.Item1;
                                    string constraint = reference.Item2;
                                    bool ask = false;
                                    bool mentionmyname = false;

                                    if (constraint == "SW")
                                    {
                                        if (i > 0) continue;
                                    }
                                    for (int y = 0; y < checkPair; y++)
                                    {
                                        dmTokens[y + i] = "";
                                    }

                                    int te = i + checkPair;
                                    if (dmTokens.Count > te)
                                    {
                                        if (dmTokens[te] == "{QUESTIONMARK}") ask = true;
                                        else if (dmTokens[te] == "{MENTIONMYNAME}") mentionmyname = true;
                                    }

                                    if (i > 0)
                                    {
                                        if (dmTokens[i - 1] == "{MENTIONMYNAME}") mentionmyname = true;
                                    }


                                    bool exists = false;
                                    foreach (Assumption ent in intent.assumptions)
                                    {
                                        if (ent.Intent == name)
                                        {
                                            exists = true;
                                            break;
                                        }
                                    }
                                    if (!exists)
                                    {
                                        intent.assumptions.Add(new Assumption(name, ask, mentionmyname));
                                    }
                                }
                            }
                        }
                    }

                    checkPair--;                    
                }

                return intent;
            }
            else
                return null;
        }
    }
}

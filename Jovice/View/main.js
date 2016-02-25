﻿(function () {

    var page;
    var logo;
    var infobox;

    ui("main", {
        init: function (p) {
            page = jovice.init(p);

            logo = ui.raphael(p)({
            });

            (function() {
                var paper = logo.paper();

                Raphael.registerFont({ "w": 113, "face": { "font-family": "Keep Calm Cufonized", "font-weight": 500, "font-stretch": "normal", "units-per-em": "360", "panose-1": "2 0 0 0 0 0 0 0 0 0", "ascent": "288", "descent": "-72", "x-height": "5", "bbox": "-18 -316 413 101.626", "underline-thickness": "18", "underline-position": "-18", "unicode-range": "U+0020-U+007E" }, "glyphs": { " ": {}, "!": { "d": "84,-288r-15,208r-32,0r-15,-208r62,0xm80,0r-54,0r0,-54r54,0r0,54", "w": 98 }, "\"": { "d": "137,-299r-11,103r-34,0r-11,-103r56,0xm66,-299r-10,103r-35,0r-10,-103r55,0", "w": 145 }, "#": { "d": "257,-76r-62,0r-16,76r-43,0r16,-76r-61,0r-16,76r-43,0r16,-76r-34,0r0,-42r43,0r11,-52r-54,0r0,-42r62,0r16,-76r43,0r-15,76r60,0r17,-76r42,0r-16,76r34,0r0,42r-43,0r-10,52r53,0r0,42xm172,-170r-61,0r-11,52r61,0", "w": 271 }, "$": { "d": "127,-168v47,16,76,38,76,92v0,45,-33,72,-74,80r0,34r-33,0r0,-34v-39,-1,-61,-15,-82,-35r32,-37v15,13,33,21,52,23r0,-76v-42,-13,-79,-36,-79,-90v0,-49,33,-74,77,-82r0,-23r33,0r0,23v27,4,48,15,64,31r-31,36v-9,-10,-21,-16,-35,-19r0,77xm98,-243v-19,5,-33,29,-20,49v3,5,11,9,20,14r0,-63xm127,-47v28,-7,27,-52,0,-61r0,61", "w": 217 }, "%": { "d": "250,-138v43,0,72,29,72,72v0,43,-28,72,-72,72v-44,0,-72,-28,-72,-72v0,-44,29,-72,72,-72xm86,-294v43,0,72,28,72,72v0,44,-29,72,-72,72v-43,0,-72,-29,-72,-72v0,-43,29,-72,72,-72xm282,-288r-193,305r-45,0r193,-305r45,0xm250,-37v16,0,29,-14,29,-29v0,-15,-14,-28,-29,-28v-15,0,-29,14,-29,28v0,15,13,29,29,29xm86,-193v16,0,29,-13,29,-29v0,-16,-14,-29,-29,-29v-15,0,-28,14,-28,29v0,15,13,29,28,29", "w": 336 }, "&": { "d": "253,-122v-2,23,-10,48,-19,62r28,31r-35,34r-24,-27v-21,17,-41,29,-81,27v-54,-3,-93,-34,-93,-88v0,-40,25,-57,53,-76v-16,-22,-30,-33,-30,-61v0,-46,35,-72,81,-72v49,0,82,29,82,76v0,38,-27,51,-57,70r40,46v2,-5,6,-14,5,-22r50,0xm128,-180v18,-9,37,-18,38,-40v1,-16,-15,-27,-31,-27v-30,0,-44,37,-19,53xm112,-125v-39,13,-36,82,13,80v19,-1,30,-4,44,-15", "w": 268 }, "'": { "d": "64,-299r-10,103r-35,0r-10,-103r55,0", "w": 71 }, "(": { "d": "76,-104v1,87,27,148,66,204r-44,0v-41,-56,-80,-111,-80,-205v0,-95,39,-149,80,-205r42,0v-37,58,-65,113,-64,206", "w": 136 }, ")": { "d": "38,-310v41,54,80,111,80,205v1,95,-39,150,-80,205r-42,0v37,-58,65,-112,64,-206v0,-87,-26,-148,-65,-204r43,0", "w": 136 }, "*": { "d": "164,-193r-21,37v-12,-10,-24,-18,-39,-25v-1,17,1,31,3,45r-41,0v1,-14,2,-29,2,-45v-13,7,-25,16,-36,25r-21,-36r39,-21v-12,-7,-25,-14,-39,-20r21,-36r36,25v0,-16,0,-30,-2,-44r41,0v-2,13,-4,28,-3,44v14,-8,27,-16,39,-25r21,36v-15,6,-29,13,-41,21v12,8,26,14,41,19", "w": 174 }, "+": { "d": "229,-108r-77,0r0,81r-54,0r0,-81r-76,0r0,-54r76,0r0,-81r54,0r0,81r77,0r0,54", "w": 246 }, ",": { "d": "23,37v16,-2,27,-18,22,-37r-23,0r0,-54r53,0v3,50,3,108,-52,95r0,-4", "w": 89 }, "-": { "d": "124,-94r-113,0r0,-48r113,0r0,48", "w": 134 }, ".": { "d": "77,0r-55,0r0,-55r55,0r0,55", "w": 91 }, "\/": { "d": "120,-314r-83,335r-44,0r83,-335r44,0" }, "0": { "d": "144,-294v83,0,123,70,123,153v0,81,-41,146,-123,146v-81,0,-122,-65,-122,-146v0,-82,38,-153,122,-153xm144,-45v90,-5,86,-193,0,-199v-86,7,-88,193,0,199", "w": 285, "k": { "Z": 19, "Y": 34, "X": 28, "W": 12, "V": 17, "T": 13, "S": 4, "J": 18, "A": 22, "8": 4, "7": 8, "5": 12, "3": 14, "2": 22 } }, "1": { "d": "117,0r-56,0r0,-226r-47,18r0,-47r103,-38r0,293", "w": 152, "k": { "r": 4, "n": 4, "m": 4, "l": 4, "j": 5, "i": 5, "g": 5, "d": 7, "Z": 8, "U": 9, "T": 6, "S": 4, "R": 11, "Q": 6, "P": 11, "O": 6, "N": 11, "M": 11, "L": 11, "K": 11, "J": 8, "I": 11, "H": 11, "G": 6, "F": 11, "E": 11, "D": 11, "C": 6, "B": 11, "9": 7, "8": 11, "6": 7, "5": 12, "4": 8, "3": 9, "2": 9, "1": 4, "0": 7 } }, "2": { "d": "122,-293v93,0,122,103,67,164v-21,23,-39,50,-59,75r96,0r0,54r-212,0r124,-148v28,-23,37,-95,-17,-94v-30,0,-50,18,-63,37r-39,-38v25,-29,51,-50,103,-50", "w": 244, "k": { "Y": 20, "W": 4, "V": 8, "Q": 4, "O": 4, "G": 4, "C": 4, "6": 8, "4": 18, "0": 6 } }, "3": { "d": "190,-149v25,10,46,29,48,62v4,87,-100,111,-171,79v-19,-9,-37,-24,-53,-43r39,-39v17,22,41,43,78,43v28,0,49,-13,49,-42v0,-34,-39,-37,-73,-38r0,-40v33,0,68,-6,68,-38v0,-57,-92,-39,-112,1r-39,-39v25,-31,53,-50,105,-50v55,0,104,28,104,83v0,31,-21,49,-43,61", "w": 259, "k": { "Z": 9, "Y": 23, "X": 14, "W": 6, "V": 10, "S": 4, "J": 5, "A": 10, "9": 4, "6": 4, "5": 8, "2": 12 } }, "4": { "d": "239,-62r-40,0r0,62r-55,0r0,-62r-133,0r0,-40r140,-186r48,0r0,178r40,0r0,48xm144,-110r0,-96r-73,95", "w": 250, "k": { "z": 6, "y": 5, "x": 5, "v": 4, "Z": 18, "Y": 13, "X": 6, "W": 7, "V": 9, "T": 16, "S": 8, "J": 10, "9": 7, "7": 7, "5": 7, "2": 14, "1": 11 } }, "5": { "d": "89,-184v68,-26,146,12,146,87v0,113,-160,132,-213,53r38,-39v13,17,29,35,60,34v33,-1,58,-16,58,-49v0,-53,-80,-61,-112,-32r-29,-29r14,-129r162,0r0,49r-118,0", "w": 249, "k": { "z": 6, "y": 7, "x": 11, "v": 6, "Z": 15, "Y": 6, "X": 6, "T": 9, "S": 4, "J": 5, "A": 10, "9": 13, "7": 4, "5": 7, "2": 17, "1": 8 } }, "6": { "d": "87,-170v62,-41,165,1,160,80v-4,58,-46,95,-107,95v-82,0,-113,-61,-115,-149v-3,-104,72,-178,172,-138v15,6,29,19,42,35r-37,37v-16,-31,-74,-47,-98,-12v-9,13,-17,31,-17,52xm139,-45v30,0,52,-18,52,-48v0,-29,-23,-47,-52,-47v-30,0,-54,17,-54,47v0,30,24,48,54,48", "w": 268, "k": { "z": 19, "y": 17, "x": 19, "w": 13, "v": 16, "t": 7, "f": 6, "Z": 10, "Y": 19, "X": 20, "V": 7, "S": 10, "J": 10, "A": 16, "9": 14, "8": 4, "6": 6, "5": 13, "3": 7, "2": 11, "0": 4 } }, "7": { "d": "216,-288r-130,288r-61,0r110,-239r-128,0r0,-49r209,0", "w": 212, "k": { "z": 8, "u": 6, "t": 5, "s": 21, "r": 9, "q": 32, "p": 7, "o": 33, "n": 9, "m": 9, "g": 30, "e": 33, "d": 36, "c": 33, "a": 17, "S": 4, "Q": 17, "O": 17, "J": 58, "G": 17, "C": 17, "A": 58, "9": 7, "8": 10, "6": 18, "5": 9, "4": 40, "3": 4, "0": 17, ".": 35, ",": 31 } }, "8": { "d": "200,-149v25,11,48,34,48,68v0,62,-49,87,-111,87v-63,0,-112,-26,-112,-87v0,-34,23,-57,49,-68v-21,-13,-41,-30,-41,-63v0,-55,48,-81,105,-81v55,0,102,26,102,81v0,33,-18,51,-40,63xm91,-205v0,50,93,46,92,0v-1,-47,-91,-49,-92,0xm83,-87v2,57,107,52,108,0v-3,-54,-105,-55,-108,0", "w": 266, "k": { "Z": 8, "Y": 21, "X": 9, "W": 4, "V": 8, "A": 6, "2": 10 } }, "9": { "d": "128,-293v81,0,112,61,115,149v3,104,-72,177,-171,138v-15,-6,-30,-18,-43,-34r38,-38v15,32,73,47,97,12v9,-13,17,-31,17,-52v-60,42,-164,0,-159,-80v5,-59,45,-95,106,-95xm130,-148v30,0,54,-18,54,-47v0,-29,-24,-48,-54,-48v-30,0,-53,18,-53,48v0,30,23,47,53,47", "w": 261, "k": { "Z": 13, "Y": 28, "X": 24, "W": 8, "V": 13, "T": 7, "J": 15, "A": 19, "5": 11, "3": 11, "2": 16 } }, ":": { "d": "77,-153r-55,0r0,-55r55,0r0,55xm77,0r-55,0r0,-55r55,0r0,55", "w": 91 }, ";": { "d": "77,-153r-55,0r0,-55r55,0r0,55xm23,37v16,-3,28,-19,22,-37r-23,0r0,-54r54,0v3,51,3,108,-53,95r0,-4", "w": 91 }, "<": { "d": "224,-27r-206,-88r0,-43r206,-88r0,52r-139,57r139,58r0,52", "w": 241 }, "=": { "d": "231,-156r-206,0r0,-48r206,0r0,48xm231,-68r-206,0r0,-48r206,0r0,48", "w": 256 }, ">": { "d": "224,-114r-206,87r0,-52r139,-57r-139,-58r0,-52r206,88r0,44", "w": 241 }, "?": { "d": "14,-262v52,-60,204,-30,170,73v-13,39,-51,51,-80,72v-6,6,-5,24,-5,37r-51,0v-2,-44,3,-78,36,-87v22,-11,50,-20,50,-48v0,-41,-70,-36,-88,-10xm100,0r-54,0r0,-54r54,0r0,54", "w": 203 }, "@": { "d": "397,-119v0,75,-36,124,-108,124v-25,0,-43,-12,-51,-31v-12,18,-38,31,-66,31v-50,0,-78,-33,-78,-84v0,-71,42,-134,111,-134v21,0,40,6,50,20r3,-15r47,0r-30,148v-4,18,4,29,19,29v47,0,66,-38,66,-89v0,-88,-59,-142,-146,-142v-96,0,-158,62,-158,158v0,96,62,153,158,158v35,1,63,-12,84,-24r27,26v-27,18,-66,37,-111,35v-117,-5,-196,-77,-196,-195v0,-118,80,-196,196,-196v111,0,183,70,183,181xm145,-84v-6,61,76,56,85,12r17,-81v-7,-12,-22,-20,-40,-19v-43,4,-58,46,-62,88", "w": 415 }, "A": { "d": "319,0r-61,0r-27,-58r-139,0r-27,58r-61,0r136,-288r43,0xm210,-104r-48,-104r-49,104r97,0", "w": 319, "k": { "y": 32, "w": 22, "v": 30, "t": 5, "d": 12, "Y": 69, "W": 43, "V": 52, "U": 21, "T": 47, "Q": 23, "O": 23, "G": 23, "C": 23, "9": 6, "8": 10, "6": 21, "5": 4, "4": 9, "3": 4, "1": 19, "0": 22 } }, "B": { "d": "194,-149v23,12,40,34,40,67v0,52,-37,82,-89,82r-116,0r0,-288v92,-1,193,-11,191,82v0,24,-12,45,-26,57xm85,-166v41,1,80,1,80,-39v-1,-39,-38,-43,-80,-41r0,80xm85,-46v44,1,91,2,91,-40v0,-43,-45,-42,-91,-40r0,80", "w": 244, "k": { "Z": 6, "Y": 19, "X": 4, "V": 6, "2": 8 } }, "C": { "d": "172,-48v38,1,65,-20,81,-44r39,40v-25,33,-66,57,-120,57v-92,0,-154,-58,-154,-149v0,-113,114,-180,222,-134v21,9,37,23,51,40r-38,40v-16,-23,-44,-42,-81,-42v-59,2,-98,39,-98,96v0,58,40,94,98,96", "w": 292, "k": { "z": 7, "x": 5, "v": 6, "g": 17, "Y": 10, "X": 4, "Q": 12, "O": 12, "G": 12, "C": 12, "6": 7, "0": 9 } }, "D": { "d": "149,-288v86,3,136,59,136,144v0,85,-50,144,-136,144r-120,0r0,-288r120,0xm85,-48v86,8,143,-22,143,-97v0,-74,-56,-108,-143,-99r0,196", "w": 299, "k": { "Z": 23, "Y": 38, "X": 33, "W": 11, "V": 17, "T": 17, "J": 24, "A": 24, "7": 12, "5": 10, "3": 18, "2": 26 } }, "E": { "d": "219,0r-190,0r0,-288r190,0r0,46r-135,0r0,69r133,0r0,45r-133,0r0,79r135,0r0,49", "w": 233, "k": { "9": 5, "8": 6, "4": 13 } }, "F": { "d": "219,-240r-135,0r0,67r133,0r0,46r-133,0r0,127r-55,0r0,-288r190,0r0,48", "w": 226, "k": { "x": 12, "J": 95, "A": 36, "8": 9, "5": 15, "4": 11, "3": 5, ".": 29, ",": 35 } }, "G": { "d": "73,-144v-4,86,111,124,167,70r0,-28r-59,0r0,-47r113,0r0,96v-28,32,-64,58,-122,58v-92,0,-154,-58,-154,-149v0,-113,114,-180,222,-134v21,9,38,23,52,40r-38,39v-20,-27,-41,-41,-82,-41v-59,0,-95,38,-99,96", "w": 308, "k": { "z": 10, "x": 9, "Y": 17, "X": 10, "A": 5, "9": 7, "2": 4 } }, "H": { "d": "254,0r-56,0r0,-127r-113,0r0,127r-56,0r0,-288r56,0r0,114r113,0r0,-114r56,0r0,288", "w": 279 }, "I": { "d": "85,0r-56,0r0,-288r56,0r0,288", "w": 109 }, "J": { "d": "44,-73v18,37,87,29,87,-21r0,-194r57,0r0,192v7,101,-132,132,-181,59", "w": 209, "k": { "J": 4, "A": 10, "5": 6 } }, "K": { "d": "273,0r-73,0r-115,-127r0,127r-56,0r0,-288r56,0r0,129r117,-129r67,0r-130,141", "w": 268, "k": { "y": 28, "w": 24, "v": 32, "t": 6, "q": 14, "o": 17, "e": 17, "d": 19, "c": 16, "Q": 39, "O": 39, "J": 6, "G": 39, "C": 39, "9": 14, "8": 16, "6": 29, "5": 12, "4": 26, "3": 15, "0": 33 } }, "L": { "d": "224,0r-195,0r0,-288r57,0r0,234r138,0r0,54", "w": 227, "k": { "y": 30, "w": 23, "v": 28, "t": 5, "d": 5, "Y": 82, "W": 47, "V": 60, "U": 14, "T": 47, "Q": 19, "O": 19, "G": 19, "C": 19, "9": 5, "7": 5, "6": 14, "4": 104, "1": 31, "0": 16 } }, "M": { "d": "316,0r-55,0r0,-192r-90,153r-87,-155r0,194r-55,0r0,-288r63,0r80,143r83,-143r61,0r0,288", "w": 341 }, "N": { "d": "268,0r-55,0r-129,-202r0,202r-55,0r0,-288r62,0r122,188r0,-188r55,0r0,288", "w": 293 }, "O": { "d": "171,-293v92,0,153,59,153,149v0,91,-61,149,-153,149v-92,0,-153,-58,-153,-149v0,-91,62,-149,153,-149xm171,-48v58,0,96,-38,96,-96v0,-58,-38,-96,-96,-96v-59,0,-97,39,-97,96v0,57,38,96,97,96", "w": 338, "k": { "Z": 22, "Y": 37, "X": 32, "W": 10, "V": 17, "T": 16, "J": 23, "A": 23, "7": 11, "5": 10, "3": 18, "2": 25 } }, "P": { "d": "132,-288v57,4,98,37,98,97v0,76,-59,105,-145,97r0,94r-56,0r0,-288r103,0xm85,-137v52,5,90,-9,90,-55v0,-45,-39,-59,-90,-54r0,109", "w": 237, "k": { "Z": 4, "Y": 19, "X": 20, "V": 4, "J": 90, "A": 44, "5": 10, "4": 21, "3": 9, "2": 6, ".": 32, ",": 37 } }, "Q": { "d": "170,-293v91,0,151,58,151,149v0,38,-13,67,-31,90r29,29r-33,33r-30,-30v-22,15,-50,29,-86,27v-90,-4,-152,-59,-152,-149v0,-90,60,-149,152,-149xm72,-144v0,74,80,120,147,83r-27,-26r33,-33r27,27v39,-65,-8,-151,-82,-148v-58,2,-98,40,-98,97", "w": 335, "k": { "Y": 35, "W": 9, "V": 15, "T": 14, "7": 8, "5": 7, "3": 10, "2": 8 } }, "R": { "d": "229,-205v0,39,-25,63,-54,76v49,18,44,88,71,129r-62,0v-22,-50,-15,-126,-100,-114r0,114r-55,0r0,-288r114,0v52,2,86,32,86,83xm84,-157v46,2,88,0,88,-44v0,-45,-41,-49,-88,-46r0,90", "w": 250, "k": { "d": 6, "Y": 18, "V": 5, "8": 5 } }, "S": { "d": "175,-221v-19,-30,-110,-31,-91,22v40,46,137,34,137,123v0,100,-155,97,-207,43r40,-40v21,17,38,24,70,26v30,2,53,-35,27,-53v-45,-31,-126,-33,-128,-110v-2,-95,139,-105,190,-50", "w": 231, "k": { "z": 4, "y": 12, "w": 7, "v": 10, "X": 4, "9": 4, "2": 9 } }, "T": { "d": "226,-238r-79,0r0,238r-57,0r0,-238r-79,0r0,-50r215,0r0,50", "w": 233, "k": { "z": 24, "y": 26, "x": 28, "w": 27, "v": 28, "u": 26, "t": 22, "s": 31, "r": 26, "q": 39, "p": 31, "o": 34, "n": 29, "m": 29, "g": 38, "e": 36, "d": 40, "c": 34, "a": 31, "Q": 16, "O": 16, "J": 56, "G": 16, "C": 16, "A": 47, "8": 5, "6": 13, "5": 15, "4": 55, "0": 13, ".": 35, ",": 32 } }, "U": { "d": "153,-50v104,1,65,-142,72,-238r56,0r0,156v-1,83,-45,137,-127,137v-83,0,-129,-54,-129,-135r0,-158r56,0v7,96,-31,238,72,238", "w": 299, "k": { "J": 13, "A": 17, "5": 9 } }, "V": { "d": "277,-288r-109,288r-51,0r-112,-288r62,0r75,202r74,-202r61,0", "w": 279, "k": { "z": 6, "u": 4, "s": 17, "r": 8, "q": 27, "p": 5, "o": 28, "n": 8, "m": 8, "g": 26, "e": 28, "d": 31, "c": 27, "a": 19, "S": 5, "Q": 16, "O": 16, "J": 58, "G": 16, "C": 16, "A": 52, "9": 7, "8": 11, "6": 17, "5": 12, "4": 39, "3": 5, "0": 16, ".": 37, ",": 46 } }, "W": { "d": "397,-288r-94,288r-49,0r-53,-178r-53,178r-49,0r-94,-288r59,0r61,186r53,-186r46,0r52,186r62,-186r59,0", "w": 398, "k": { "s": 10, "q": 19, "o": 20, "g": 18, "e": 20, "d": 23, "c": 20, "a": 13, "Q": 10, "O": 10, "J": 46, "G": 11, "C": 10, "A": 43, "8": 8, "6": 13, "5": 10, "4": 30, "0": 12, ".": 23, ",": 32 } }, "X": { "d": "284,0r-68,0r-72,-109r-72,109r-68,0r105,-149r-97,-139r68,0r64,98r64,-98r68,0r-97,138", "w": 284, "k": { "y": 19, "w": 18, "v": 22, "t": 6, "q": 10, "o": 12, "e": 12, "d": 15, "c": 12, "Q": 32, "O": 32, "G": 32, "C": 32, "9": 10, "8": 13, "6": 26, "5": 9, "4": 17, "3": 9, "0": 28 } }, "Y": { "d": "291,-288r-118,170r0,118r-56,0r0,-118r-112,-170r65,0r75,121r78,-121r68,0", "w": 292, "k": { "z": 32, "y": 23, "x": 27, "w": 22, "v": 26, "u": 32, "t": 34, "s": 51, "r": 35, "q": 66, "p": 31, "o": 67, "n": 35, "m": 35, "g": 62, "f": 6, "e": 67, "d": 68, "c": 67, "a": 46, "S": 19, "Q": 39, "O": 39, "J": 84, "G": 40, "C": 39, "A": 75, "9": 24, "8": 27, "6": 36, "5": 18, "4": 84, "3": 21, "2": 17, "0": 36, ".": 55, ",": 46 } }, "Z": { "d": "250,-288r-139,233r138,0r0,55r-235,0r143,-238r-137,0r0,-50r230,0", "w": 260, "k": { "y": 19, "w": 18, "v": 22, "Q": 15, "O": 15, "G": 15, "C": 15, "6": 11, "4": 80, "0": 13 } }, "[": { "d": "131,92r-109,0r0,-402r109,0r0,44r-59,0r0,315r59,0r0,43", "w": 136 }, "\\": { "d": "120,21r-44,0r-83,-335r44,0" }, "]": { "d": "114,92r-109,0r0,-43r59,0r0,-316r-59,0r0,-43r109,0r0,402", "w": 135 }, "^": { "d": "370,-139v0,-7,12,-8,12,0v0,4,-2,6,-6,6v-4,0,-5,-3,-6,-6xm354,-153v0,-10,14,-8,15,0v-1,8,-15,10,-15,0xm343,-158v-7,0,-10,-15,0,-15v10,0,9,14,0,15xm315,-179v0,-12,16,-9,17,0v1,4,-3,10,-8,9v-5,1,-9,-5,-9,-9xm302,-199v11,0,12,20,0,19v-5,0,-10,-4,-10,-9v0,-5,5,-10,10,-10xm276,-209v5,0,10,5,10,10v0,11,-19,12,-19,0v-1,-6,4,-10,9,-10xm249,-217v5,0,10,5,10,10v0,5,-5,10,-10,10v-5,0,-10,-5,-10,-10v0,-5,5,-10,10,-10xm241,-185v-9,7,-6,45,-19,40v4,-16,7,-36,13,-50v56,14,99,36,136,68v-3,6,-6,4,-10,0v-33,-27,-73,-47,-120,-58xm210,-198v-28,0,-30,-42,-4,-43r0,-13r-20,7r-1,-23r18,5r-8,-23r28,0r-7,23r17,-5r0,23r-20,-7r0,13v9,0,19,11,19,21v0,12,-10,22,-22,22xm200,-185v0,-12,19,-11,19,0v0,5,-5,10,-10,10v-4,0,-9,-5,-9,-10xm168,-218v5,0,10,5,10,10v0,5,-5,10,-10,10v-5,0,-10,-5,-10,-10v0,-5,5,-10,10,-10xm209,-172v4,0,9,3,8,8v1,4,-4,7,-7,7v-11,1,-12,-15,-1,-15xm210,-140v-8,0,-9,-13,-1,-13v8,-1,9,13,1,13xm141,-211v13,0,15,20,1,20v-5,0,-10,-5,-10,-10v0,-5,4,-10,9,-10xm107,-191v0,-11,19,-11,19,0v0,5,-4,10,-9,10v-5,0,-10,-5,-10,-10xm183,-194v6,14,11,32,13,49v-13,4,-10,-33,-19,-40v-48,11,-90,34,-123,61v-8,-14,23,-22,31,-31v28,-18,59,-31,98,-39xm95,-188v4,-1,9,4,9,8v0,4,-5,9,-9,9v-4,0,-9,-5,-8,-9v-1,-4,4,-9,8,-8xm305,-60v-4,32,47,37,63,19v6,-7,11,-17,14,-31v-10,5,-17,6,-26,11r13,-47v6,5,13,19,19,16r-4,-30v10,2,21,3,29,7v-11,16,-6,45,-21,55v-8,19,-20,37,-10,61v-2,16,-16,22,-22,34v-77,-37,-220,-38,-298,-1v-19,-13,-36,-25,-27,-61v-2,-25,-27,-38,-20,-64r-8,-24r31,-8v-3,11,-10,23,-10,33v6,-1,17,-12,20,-17r9,45v-6,-5,-13,-7,-23,-9v1,41,78,56,78,8v0,-10,-7,-20,-17,-20v-15,0,-19,14,-16,28v-28,-7,-19,-56,12,-56v8,0,10,2,15,6v-11,-14,-6,-40,4,-51v15,8,24,29,16,50v11,-18,44,-13,44,12v0,8,-8,22,-14,20v0,-9,-7,-16,-16,-16v-9,0,-15,7,-14,17v3,39,77,36,76,-5r-1,-7r-23,11r-2,-44v9,5,15,13,24,13r-12,-32r41,0v-4,11,-11,24,-10,34r22,-15r-1,44r-22,-11v-2,25,10,41,33,40v29,6,49,-35,21,-43v-7,0,-13,9,-15,14v-21,-16,-1,-58,25,-40v4,3,6,6,9,9v-5,-22,6,-39,18,-49v13,11,12,40,2,55v13,-22,53,-1,40,24v-5,10,-12,19,-23,22v5,-8,7,-28,-8,-28v-10,0,-14,13,-15,21xm68,-167v0,-10,14,-9,15,0v0,4,-3,7,-7,7v-3,0,-9,-2,-8,-7xm58,-162v7,0,10,15,0,15v-10,0,-8,-14,0,-15xm44,-134v-8,0,-8,-14,0,-13v8,0,8,13,0,13xm71,41v66,-36,201,-32,272,-5v4,1,5,3,5,5v-70,36,-204,29,-277,0xm387,-68v6,3,11,-6,10,-15v-7,-6,-10,5,-11,12v0,1,1,2,1,3xm213,-220v7,1,17,-5,8,-10v-3,-4,-10,-8,-8,3r0,7xm206,-220r0,-15v-6,2,-10,7,-12,13v3,2,7,2,12,2xm196,-211v4,7,22,10,26,0v-6,-5,-19,-5,-26,0xm367,14v6,-6,11,-15,5,-26r-12,12xm337,-7v4,0,7,-3,7,-7v0,-4,-3,-7,-7,-7v-5,0,-7,2,-7,7v0,4,3,7,7,7xm324,5v0,10,14,10,14,0v1,-4,-4,-7,-7,-7v-4,0,-7,3,-7,7xm271,-8v15,19,55,2,35,-18v-13,-18,-55,0,-35,18xm210,-89v4,0,6,-2,6,-6v0,-8,-13,-8,-13,0v0,4,2,7,7,6xm238,-29v0,6,8,9,12,5v4,-4,0,-12,-6,-12v-5,0,-6,2,-6,7xm235,-9v0,10,14,10,14,0v1,-4,-4,-7,-7,-7v-4,0,-7,3,-7,7xm209,-8v11,-7,12,-26,0,-33v-13,8,-10,26,0,33xm166,-29v0,6,8,9,12,5v4,-4,0,-12,-6,-12v-5,0,-6,2,-6,7xm175,-1v10,0,8,-16,0,-15v-10,0,-10,15,0,15xm113,-2v21,12,51,-16,28,-29v-20,-12,-51,14,-28,29xm21,-86v-6,5,0,22,8,17v2,-7,-2,-16,-8,-17xm69,-13v0,4,3,7,7,7v5,0,7,-2,7,-7v0,-4,-3,-7,-7,-7v-4,0,-7,3,-7,7xm75,6v0,8,15,10,15,0v1,-5,-5,-7,-8,-7v-4,0,-7,3,-7,7xm52,17v5,-13,-3,-23,-11,-27v-3,13,1,21,11,27", "w": 419 }, "_": { "d": "209,59r-218,0r0,-35r218,0r0,35", "w": 199 }, "`": { "d": "133,-234r-38,0r-52,-69r58,0", "w": 208 }, "a": { "d": "100,-213v62,1,91,36,91,98r0,115r-52,0r0,-11v-43,31,-133,18,-128,-47v4,-60,61,-72,128,-70v2,-47,-68,-50,-91,-21r-31,-31v18,-19,48,-33,83,-33xm63,-62v0,34,57,27,76,10r0,-38v0,0,-76,-4,-76,28", "w": 208, "k": { "1": 23 } }, "b": { "d": "74,-198v65,-44,148,16,148,94v0,79,-81,137,-148,93r0,11r-52,0r0,-299r52,0r0,101xm74,-56v37,35,95,6,95,-48v0,-55,-55,-80,-95,-52r0,100", "w": 232, "k": { "x": 7, "9": 12, "7": 27, "5": 8, "2": 32, "1": 27 } }, "c": { "d": "67,-104v0,62,80,83,107,37r36,36v-19,21,-48,36,-85,36v-67,0,-107,-43,-111,-109v-7,-100,130,-143,193,-75r-35,36v-27,-43,-105,-22,-105,39", "w": 213, "k": { "7": 6, "4": 8, "2": 9, "1": 13 } }, "d": { "d": "14,-104v0,-78,78,-136,148,-96r0,-99r53,0r0,299r-53,0r0,-11v-65,44,-148,-14,-148,-93xm67,-104v0,54,57,83,95,48r0,-100v-38,-28,-95,-3,-95,52", "w": 236 }, "e": { "d": "68,-83v7,43,79,58,111,26r33,33v-24,19,-49,29,-86,29v-68,0,-112,-43,-112,-109v0,-65,44,-109,111,-109v72,0,120,51,110,130r-167,0xm182,-125v-8,-35,-59,-58,-92,-31v-10,8,-18,18,-22,31r114,0", "w": 247, "k": { "x": 8, "9": 9, "7": 27, "5": 18, "3": 17, "2": 27, "1": 24 } }, "f": { "d": "32,-208v-2,-57,11,-97,66,-96v12,0,26,2,40,5r0,48v-24,-9,-59,-9,-54,27r0,16r33,0r0,46r-33,0r0,162r-52,0r0,-162r-25,0r0,-46r25,0", "w": 132, "k": { "o": 7, "e": 7, "4": 17 } }, "g": { "d": "66,-22v-22,-5,-19,-35,1,-40v-24,-13,-48,-34,-48,-70v0,-59,60,-94,122,-76r72,0r0,41r-22,0v27,67,-23,112,-85,119v-14,13,4,18,18,20v46,3,85,21,85,64v0,87,-196,88,-196,0v0,-35,20,-51,53,-58xm109,-94v23,0,39,-15,39,-38v0,-23,-16,-39,-39,-39v-23,0,-38,16,-38,39v0,22,15,38,38,38xm65,36v2,33,91,34,92,0v0,-34,-90,-34,-92,0", "w": 216, "k": { "7": 4, "3": 21 } }, "h": { "d": "130,-214v52,2,80,33,80,86r0,128r-52,0v-7,-62,26,-166,-41,-169v-19,0,-31,5,-43,13r0,156r-52,0r0,-298r52,0r0,102v13,-11,34,-18,56,-18", "w": 227, "k": { "9": 6, "7": 5, "1": 24 } }, "i": { "d": "76,-244r-54,0r0,-54r54,0r0,54xm75,0r-53,0r0,-208r53,0r0,208", "w": 100 }, "j": { "d": "80,-244r-54,0r0,-54r54,0r0,54xm-18,40v24,9,45,-2,45,-30r0,-218r52,0v-3,84,10,180,-5,255v-6,30,-33,48,-75,42", "w": 105, "k": { "5": 4 } }, "k": { "d": "220,0r-62,0r-83,-93r0,93r-53,0r0,-298r53,0r0,178r72,-88r61,0r-86,101", "w": 216, "k": { "q": 8, "o": 12, "e": 12, "d": 22, "c": 11, "5": 13, "4": 28, "3": 18 } }, "l": { "d": "75,-61v-2,24,19,28,34,17r0,43v-39,17,-87,-1,-87,-51r0,-247r53,0r0,238", "k": { "y": 11, "w": 11, "v": 11, "t": 7, "f": 5, "4": 9, "1": 6, "0": 6 } }, "m": { "d": "249,-213v102,-5,78,118,80,213r-51,0v-7,-66,29,-190,-59,-165v-7,2,-14,6,-21,12v7,45,1,103,3,153r-52,0v-7,-60,25,-169,-36,-169v-19,0,-29,7,-40,17r0,152r-51,0r0,-208r51,0r0,15v24,-28,88,-25,107,6v19,-17,38,-25,69,-26", "w": 347, "k": { "1": 17 } }, "n": { "d": "74,-193v48,-42,137,-13,137,66r0,127r-52,0v-7,-63,25,-166,-41,-169v-18,0,-34,7,-44,17r0,152r-52,0r0,-208r52,0r0,15", "w": 228, "k": { "1": 19 } }, "o": { "d": "125,-213v67,0,112,43,112,109v0,66,-45,109,-112,109v-67,0,-111,-43,-111,-109v0,-66,44,-109,111,-109xm125,-41v37,0,60,-26,60,-63v0,-36,-22,-63,-60,-63v-38,0,-59,27,-59,63v0,37,22,63,59,63", "w": 247, "k": { "x": 9, "9": 5, "7": 23, "5": 4, "2": 30, "1": 21 } }, "p": { "d": "222,-104v0,80,-81,137,-148,94r0,100r-52,0r0,-298r52,0r0,10v66,-42,148,16,148,94xm74,-55v37,34,95,5,95,-48v0,-55,-56,-81,-95,-52r0,100", "w": 232, "k": { "y": 5, "x": 7, "9": 5, "7": 22, "2": 28, "1": 21 } }, "q": { "d": "14,-104v-3,-78,78,-134,148,-96r0,-8r52,0r0,298r-52,0r0,-100v-12,10,-27,16,-48,15v-65,-2,-97,-46,-100,-109xm67,-103v0,53,57,82,95,48r0,-100v-38,-29,-95,-4,-95,52", "w": 235 }, "r": { "d": "74,-186v17,-36,77,-32,97,0r-27,43v-20,-31,-70,-23,-70,26r0,117r-52,0r0,-208r52,0r0,22", "w": 167, "k": { "7": 57, "4": 22, "3": 36, "2": 33 } }, "s": { "d": "141,-146v-11,-23,-65,-38,-75,-5v10,31,70,25,88,51v11,10,20,23,20,42v-2,77,-126,81,-159,27r31,-32v11,14,26,23,48,25v24,2,40,-22,17,-33v-36,-18,-97,-24,-97,-78v0,-76,122,-82,158,-28", "w": 184, "k": { "7": 10, "2": 13, "1": 10 } }, "t": { "d": "85,-67v-5,34,36,29,53,17r0,45v-9,6,-24,10,-40,10v-84,0,-62,-91,-65,-167r-28,0r0,-19v32,-15,59,-46,64,-88r16,0r0,61r52,0r0,46r-52,0r0,95", "w": 146, "k": { "4": 19 } }, "u": { "d": "70,-208v8,62,-26,166,41,168v17,0,31,-5,43,-17r0,-151r52,0r0,208r-52,0r0,-16v-13,11,-31,21,-55,21v-53,-1,-81,-33,-81,-87r0,-126r52,0", "w": 227 }, "v": { "d": "213,-208r-78,208r-51,0r-79,-208r57,0r47,140r48,-140r56,0", "w": 214, "k": { "7": 50, "5": 4, "4": 8, "3": 21, "2": 18 } }, "w": { "d": "318,-208r-70,208r-45,0r-41,-124r-41,124r-45,0r-71,-208r55,0r39,130r40,-130r46,0r39,130r40,-130r54,0", "w": 320, "k": { "7": 43, "4": 4, "3": 15, "2": 12 } }, "x": { "d": "211,0r-59,0r-44,-66r-43,66r-60,0r75,-109r-68,-99r58,0r38,58r38,-58r59,0r-68,99", "w": 212, "k": { "q": 7, "o": 9, "e": 9, "d": 7, "c": 9, "5": 8, "4": 12, "3": 9 } }, "y": { "d": "211,-208r-94,247v-12,37,-39,58,-84,50r-17,-48v40,19,63,-9,70,-41r-81,-208r57,0r48,133r45,-133r56,0", "w": 212, "k": { "q": 9, "7": 51, "4": 9, "3": 16, "2": 13 } }, "z": { "d": "195,-208r-103,162r99,0r0,46r-184,0r101,-162r-91,0r0,-46r178,0", "w": 200, "k": { "4": 33 } }, "{": { "d": "140,51r0,48v-83,10,-88,-46,-88,-125v0,-32,-8,-58,-41,-58r0,-41v45,1,40,-47,40,-91v0,-62,20,-99,89,-91r0,47v-78,-10,-8,134,-72,156v65,22,-5,164,72,155", "w": 145 }, "|": { "d": "74,70r-44,0r0,-384r44,0r0,384", "w": 96 }, "}": { "d": "94,-216v0,43,-5,93,41,91r0,41v-45,-1,-42,47,-41,92v2,62,-19,100,-89,91r0,-48v78,12,6,-136,73,-155v-31,-13,-32,-53,-33,-95v-1,-37,0,-66,-40,-61r0,-47v69,-7,89,28,89,91", "w": 145 }, "~": { "d": "225,-123v-30,36,-98,22,-135,3v-33,-17,-63,8,-81,25r0,-57v34,-41,97,-21,136,-4v34,15,61,-9,80,-26r0,59", "w": 234 }, "\u00a0": {} } });

                var font = paper.getFont("Keep Calm Cufonized", 800);
                var accent = ui.color(50);

                var os = "TELKOM.CENTER";
                var ol = "1332353345140";
                var el = [];
                var tr = [];
                                
                var left = 0;
                for (var i = 0; i < os.length; i++) {
                    var c = os[i];

                    el[i] = paper.print(0, 0, c, font, 30).attr({ fill: accent });
                    
                    var bbox = el[i].getBBox();
                    
                    tr[i] = "t" + (left + 10) + ",28";
                    el[i].transform(tr[i]);

                    left += bbox.width + parseInt(ol[i]);
                }                
                logo.size(left + 30, 50);
            })();

            infobox = ui.box(p)({
                height: 50,
            });

            (function () {

                var wd = ui.text(infobox)({
                    position: [20, 5],
                    text: "WE'VE INDEXED",
                    hide: true,
                    font: 12,
                    color: 50
                });
                var oc = ui.text(infobox)({ top: 0, text: "1000", hide: true, font: 18, color: 20 });
                var ocX = ui.text(infobox)({ top: 0, text: "1000 DUMMY", hide: true, font: 18, color: 20 });
                var de = ui.text(infobox)({ top: 5, text: "DESCS", hide: true, font: 12, color: 50 });
                var deX = ui.text(infobox)({ top: 5, text: "DESCS DUMMY", hide: true, font: 12, color: 50 });

                oc.left(wd.textSize().width + wd.left() + 10);
                ocX.left(wd.textSize().width + wd.left() + 10);

                $$.get(5001, function (d) {

                    wd.show();
                    wd.$.css({ x: 10, opacity: 0 }).transition({ x: 0, opacity: 1, duration: 100 });
                    
                    //debug(d);
                    
                    var idx = 0;
                    var len = d.counts.length;

                    $$(function () {
                        var cou = d.counts[idx];
                        var des = d.descs[idx];

                        if (de.isShown()) {
                            
                            ocX.show();
                            ocX.text(oc.text());
                            deX.show();
                            deX.text(de.text());
                            deX.left(de.left());
                            
                            ocX.$.css({ x: 0, opacity: 1 }).transition({ x: -10, opacity: 0, duration: 1000 });
                            deX.$.css({ x: 0, opacity: 1 }).transition({ x: -10, opacity: 0, duration: 1000 });
                        }

                        oc.show();
                        oc.text(cou + "");
                        de.show();
                        de.text(des + "");

                        de.left(oc.textSize().width + oc.left() + 5);

                        oc.$.css({ x: 10, opacity: 0 }).transition({ x: 0, opacity: 1, duration: 1000 });
                        de.$.css({ x: 10, opacity: 0 }).transition({ x: 0, opacity: 1, duration: 1000 });

                        idx++;
                        if (idx == len) idx = 0;

                        return 5000;
                    }, -1);
                });

            })();
            
            jovice.main(function () {
                logo.$.transition({ x: -10, opacity: 0, duration: 50 });
                infobox.hide();
            });

            logo.$.css({ x: 10, opacity: 0 }).transition({ x: 0, opacity: 1, duration: 200 });

            p.done();
        },
        start: function (p) {
            jovice.setSearchBoxValue(null);
            p.done();
        },
        resize: function (p) {
            var w = p.width();
            logo.top(110);
            infobox.top(230);
            if (w < 840) {
                logo.left(20);
                infobox.leftRight(20, 20);
                infobox.width(null);
            }
            else {
                logo.left((w - 800) / 2);
                infobox.left((w - 800) / 2);
                infobox.width(800);
            }
        }
    });
})();
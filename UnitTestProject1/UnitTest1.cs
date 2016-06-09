using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Media;
using RiftChatMetro;
using System.Windows.Controls;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> contentL = new List<string>();
            contentL.Add("04:26:12: [Verföhnt] hat sich eingeloggt.");
            contentL.Add("08:13:15: [Filysha] hat sich eingeloggt.");
            contentL.Add("04:26:12: [16. Level 65][Shaslol@Zaviel]: tentakelrapeforlife");
            contentL.Add("04:26:12: [Gilde][Shaslol@Zaviel]: tentakelrapeforlife");
            contentL.Add("07:20:34: [10. Level 50-59@Typhiria][@Typhiria]: Das Zonenereignis [Instabil: Glutinsel] in [Glutinsel] hat auf Typhiria begonnen!");
            contentL.Add("04:26:12: [16. Level 65][Sheide@Zaviel]: we all have our secrets :D");
            contentL.Add("04:26:16: [16. Level 65][Wedancegj@Gelidra]: w8t one sec zaviel are u he or she :D");
            contentL.Add("04:26:19: [Artoine@Typhiria] flüstert: ich hab programmieren als haupt- und nebenfach in" +
                         "der schule aber da macht man nicht so schnell progress :D auf ner uni gehts schon tausendmal schneller vorwärts");
            contentL.Add("04:26:20: [4. Stufe 1-29][Raphaely]: Also ein paar k platin ansammeln und auktionshaus preiskontrolle veranstalten...");
            contentL.Add("15:39:07: [Bobito] hat beim Bedarfs-Wurf 44 gewürfelt für: [Dicker Goldgürtel]");
            contentL.Add("15:39:07: [Bobito] hat mit einem Bedarfs-Wurf von 44 Folgendes gewonnen: [Dicker Goldgürtel]");
            contentL.Add("15:39:07: [Bobito] hat Folgendes erbeutet: [Dicker Goldgürtel]");
            contentL.Add("15:39:15: [Bobito] hat beim Bedarfs-Wurf 3 gewürfelt für: [Güldener Zermalmer]");
            contentL.Add("15:39:15: [Bobito] hat mit einem Bedarfs-Wurf von 3 Folgendes gewonnen: [Güldener Zermalmer]");

            MacroManager.Initialize();
            LineEvaluator lEval = new LineEvaluator();
            ContentControls dw = new ContentControls("chattest");
            dw.add("global", new DataGrid());

            lEval.registerCustomMask("lfm", Brushes.Orange);

            foreach (var item in contentL)
            {
                Line line = lEval.createLine(item);
                dw.write(line);
            }
        }
    }
}

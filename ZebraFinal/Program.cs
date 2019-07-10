using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebraFinal
{
    class Program
    {
        static int states = 0;

        static Boolean AssignComplete(Dictionary<int, List<String>> CSP)
        {
            foreach (var item in CSP)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (CSP[item.Key][i] == null || CSP[item.Key][i] == "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static int[] NextUnassignedVar(Dictionary<int, List<String>> CSP)
        {
            // e kthen nje array me dy elemente
            int[] values = new int[2];

            foreach (var item in CSP)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (CSP[item.Key][i] == null) // nese njeri atribut i shtepise perkatese eshte null
                    {
                        values[0] = item.Key; // nr. i shtepise [1, 2, 3, 4 apo 5]
                        values[1] = i; // e kthen atributin qe eshte i thate, per shembull, 'color' apo 'nationality' apo 'animal' etj.

                        return values;
                    }
                }
            }
            return values;
        }

        static Boolean CheckConsistency(Dictionary<int, List<String>> CSP, int house, String var, String val, int varNo)
        {
            // var -> 'nac', 'ngjyra' e te tjera
            // val -> for specific var, the specific variable ['e kuqe', 'zebra', etj]

            // nese per shembull: kal = kal, kthehet return false
            foreach (var item in CSP)
            {
                if (CSP[item.Key][varNo] != null && CSP[item.Key][varNo].Equals(val))
                    return false;
            }

            // Constraint number 1: nese ngjyra e kuqe dhe shtepia me numer 'house' nuk e ka atributin e pare angleze =>
            if (var.Equals("ngjyra") && val.Equals("e kuqe"))
            {
                if (CSP[house][0] != null && !CSP[house][0].Equals("anglez"))
                    return false;
            } // nese nacionaliteti eshte anglez dhe ngjyra e kuqe nuk i perket shtepise 'house' =>
            if (var.Equals("nac") && val.Equals("anglez"))
            {
                if (CSP[house][2] != null && !CSP[house][2].Equals("e kuqe"))
                    return false;
            }

            // Constraint number 2: nese kafsha eshte qen dhe nacionaliteti i klases nuk eshte spanjoll =>
            if (var.Equals("kafsha") && val.Equals("qen"))
            {
                if (CSP[house][0] != null && !CSP[house][0].Equals("spanjoll"))
                    return false;
            } // nese nacionaliteti eshte spanjoll dhe kafsha nuk eshte qen =>
            if (var.Equals("nac") && val.Equals("spanjoll"))
            {
                if (CSP[house][1] != null && !CSP[house][1].Equals("qen"))
                    return false;
            }

            // Constraint number 3: norvegjezi te shtepia 1
            if (var.Equals("nac") && val.Equals("norvegjez"))
            {
                if (house != 1) { return false; };
            }
            if (house == 1)
            {
                if (var.Equals("nac") && !val.Equals("norvegjez"))
                {
                    return false;
                }
            }

            //Constraint number 4
            if (var.Equals("ngjyra") && val.Equals("e gjelber"))
            {
                if (house == 1)
                    return false;

                if (CSP[house - 1][2] != null && !CSP[house - 1][2].Equals("e fildishte"))
                    return false;

                foreach (var item2 in CSP)
                {
                    if (CSP[item2.Key][2] != null && CSP[item2.Key][2].Equals("e fildishte") && item2.Key != house - 1)
                    {
                        return false;
                    }
                }
            }
            if (var.Equals("ngjyra") && val.Equals("e fildishte"))
            {
                if (house == 5)
                {
                    return false;
                }
                if (CSP[house + 1][2] != null && !CSP[house + 1][2].Equals("e gjelber"))
                {
                    return false;
                }

                foreach (var item2 in CSP)
                {
                    if (CSP[item2.Key][2] != null && CSP[item2.Key][2].Equals("e gjelber") && item2.Key != house + 1)
                    {
                        return false;
                    }
                }
            }

            //Constraint number 5
            if (var.Equals("duhani") && val.Equals("chesterfields"))
            {
                foreach (var item2 in CSP)
                {
                    if (CSP[item2.Key][1] != null && CSP[item2.Key][1].Equals("dhelper"))
                    {
                        if (!(item2.Key == house - 1 || item2.Key == house + 1))
                            return false;
                    }
                }
            }
            if (var.Equals("kafsha") && val.Equals("dhelper"))
            {
                foreach (var item2 in CSP)
                {
                    if (CSP[item2.Key][3] != null && CSP[item2.Key][3].Equals("chesterfields"))
                    {
                        if (!(item2.Key == house - 1 || item2.Key == house + 1))
                            return false;
                    }
                }
            }

            //Constraint Number 6
            if (var.Equals("duhani") && val.Equals("marlboro"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][2] != null && CSP[item.Key][2].Equals("e verdhe"))
                    {
                        if (!(item.Key == house))
                        {
                            return false;
                        }
                    }
                }
            }
            if (var.Equals("ngjyra") && val.Equals("e verdhe"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][3] != null && CSP[item.Key][3].Equals("marlboro"))
                    {
                        if (!(item.Key == house))
                        {
                            return false;
                        }
                    }
                }
            }

            //Constraint Number 7
            if (var.Equals("nac") && val.Equals("norvegjez"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][2] != null && CSP[item.Key][2].Equals("e kalter"))
                    {
                        if (!(item.Key == house - 1 || item.Key == house + 1))
                        {
                            return false;
                        }
                    }
                }
            }
            if (var.Equals("ngjyra") && val.Equals("e kalter"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][0] != null && CSP[item.Key][0].Equals("norvegjez"))
                    {
                        if (!(item.Key == house - 1 || item.Key == house + 1))
                        {
                            return false;
                        }
                    }
                }
            }

            //Constraint Number 8
            if (var.Equals("duhani") && val.Equals("winston"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][1] != null && CSP[item.Key][1].Equals("kermij"))
                    {
                        if (!(item.Key == house))
                        {
                            return false;
                        }
                    }
                }
            }
            if (var.Equals("kafsha") && val.Equals("kermij"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][3] != null && CSP[item.Key][3].Equals("winston"))
                    {
                        if (!(item.Key == house))
                        {
                            return false;
                        }
                    }
                }
            }

            //Constraint Number 9
            if (var.Equals("duhani") && val.Equals("luckystrike"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][4] != null && CSP[item.Key][4].Equals("portokall"))
                    {
                        if (!(item.Key == house))
                            return false;

                    }
                }
            }
            if (var.Equals("pija") && val.Equals("portokall"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][3] != null && CSP[item.Key][3].Equals("luckystrike"))
                    {
                        if (!(item.Key == house))
                            return false;

                    }
                }
            }

            //Constraint Number 10
            if (var.Equals("nac") && val.Equals("ukrainas"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][4] != null && CSP[item.Key][4].Equals("çaj"))
                    {
                        if (!(item.Key == house))
                            return false;

                    }
                }
            }
            if (var.Equals("pija") && val.Equals("çaj"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][0] != null && CSP[item.Key][0].Equals("ukrainas"))
                    {
                        if (!(item.Key == house))
                            return false;

                    }
                }
            }

            //Constraint Number 11
            if (var.Equals("nac") && val.Equals("japanez"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][3] != null && CSP[item.Key][3].Equals("parlament"))
                    {
                        if (!(item.Key == house))
                            return false;
                    }
                }
            }
            if (var.Equals("duhani") && val.Equals("parlament"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][0] != null && CSP[item.Key][0].Equals("japanez"))
                    {
                        if (!(item.Key == house))
                            return false;
                    }
                }
            }

            //Constraint Number 12
            if (var.Equals("duhani") && val.Equals("marlboro"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][1] != null && CSP[item.Key][1].Equals("kal"))
                    {
                        if (!(item.Key == house - 1 || item.Key == house + 1))
                            return false;
                    }
                }
            }
            if (var.Equals("kafsha") && val.Equals("kal"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][3] != null && CSP[item.Key][3].Equals("marlboro"))
                    {
                        if (!(item.Key == house - 1 || item.Key == house + 1))
                            return false;
                    }
                }
            }

            //Constraint Number 13
            if (var.Equals("pija") && val.Equals("kafe"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][2] != null && CSP[item.Key][2].Equals("e gjelber"))
                    {
                        if (!(item.Key == house))
                            return false;
                    }
                }
            }
            if (var.Equals("ngjyra") && val.Equals("e gjelber"))
            {
                foreach (var item in CSP)
                {
                    if (CSP[item.Key][4] != null && CSP[item.Key][4].Equals("kafe"))
                    {
                        if (!(item.Key == house))
                            return false;
                    }
                }
            }

            //Constraint Number 14
            if (var.Equals("pija") && val.Equals("qumesht"))
            {
                if (house != 3)
                    return false;
            }
            if (house == 3)
            {
                if (var.Equals("pija") && !val.Equals("qumesht"))
                    return false;
            }

            return true;
        }

        static void Answer(Dictionary<int, List<String>> CSP)
        {
            //Console.WriteLine("nacionalititeti kafsha      ngjyra       duhani      pija");
            foreach (var item in CSP)
            {
                Console.Write("Shtepia " + item.Key + ":   ");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(CSP[item.Key][i] + "  ");
                }
                Console.WriteLine("");
            }
        }

        static Dictionary<int, List<String>> Backtrack(Dictionary<int, List<String>> CSP, List<String> vars, Dictionary<String, List<String>> varDoms)
        {
            if (AssignComplete(CSP))
                return CSP;

            int[] var = NextUnassignedVar(CSP); // kthen dy variabla, var[0] - nr. shtepise, var[1] - atributi i var[0]
            int house = var[0]; // shtepia me numer x, kthehet numer [1, 2, 3, 4, 5]
            String unassignedVar = vars[var[1]]; // variabla, shembull 'nacionaliteti', 'ngjyra', etj.
            // te vars e kemi te definume nje array statik me emrat e domeneve 

            List<String> domList = varDoms[unassignedVar]; // lista e domenit perkates, per shembull:
            // nese unassignedVar == nat, domList = {'japanez', 'anglez', ... };

            foreach (var val in domList) // per secilin atribut te domenit
            {
                if (CheckConsistency(CSP, house, unassignedVar, val, var[1]))
                {
                    CSP[house][var[1]] = val; // nese funksioni ka kthy true, shoqeroja vleren shtepise
                    states = states + 1; // rrite gjendjen

                    CSP = Backtrack(CSP, vars, varDoms); // backtracking [nese kthehet false backtracku i rradhes]
                    // perseri behet null te rreshti me posht qe me rikontrollu kombinime tjera
                    if (AssignComplete(CSP)) 
                        return CSP;

                    CSP[house][var[1]] = null;
                }
            }

            return CSP;
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<String>> CSP = new Dictionary<int, List<String>>();
            Dictionary<String, List<String>> varDoms = new Dictionary<String, List<String>>();

            // krijimi i CSP modelit dhe mbushja e vlerave me null (25 copa)
            for (int i = 1; i <= 5; i++)
            {
                CSP.Add(i, new List<string>(5));
                for (int j = 0; j < 5; j++)
                {
                    CSP[i].Add(null);
                }
            }

            // variables
            List<String> vars = new List<String>();
            vars.Add("nac");
            vars.Add("kafsha");
            vars.Add("ngjyra");
            vars.Add("duhani");
            vars.Add("pija");

            // nationality domain
            List<String> nacDom = new List<String>();
            nacDom.Add("japanez");
            nacDom.Add("spanjoll");
            nacDom.Add("anglez");
            nacDom.Add("ukrainas");
            nacDom.Add("norvegjez");
            // shuffle nationality domain
            var rand = new Random();
            nacDom = nacDom.OrderBy(x => rand.Next()).ToList();
            varDoms["nac"] = nacDom;

            // animals domain
            List<String> kafshaDom = new List<String>();
            kafshaDom.Add("dhelper");
            kafshaDom.Add("qen");
            kafshaDom.Add("kermij");
            kafshaDom.Add("kal");
            kafshaDom.Add("zeber");
            // shufle animals domain
            rand = new Random();
            kafshaDom = kafshaDom.OrderBy(x => rand.Next()).ToList();
            varDoms["kafsha"] = kafshaDom;

            List<String> ngjyraDom = new List<String>();
            ngjyraDom.Add("e kuqe");
            ngjyraDom.Add("e fildishte");
            ngjyraDom.Add("e verdhe");
            ngjyraDom.Add("e kalter");
            ngjyraDom.Add("e gjelber");
            // shufle color domain
            rand = new Random();
            ngjyraDom = ngjyraDom.OrderBy(x => rand.Next()).ToList();
            varDoms["ngjyra"] = ngjyraDom;

            List<String> duhaniDom = new List<String>();
            duhaniDom.Add("winston");
            duhaniDom.Add("marlboro");
            duhaniDom.Add("parlament");
            duhaniDom.Add("luckystrike");
            duhaniDom.Add("chesterfields");
            // shufle duhani domain
            rand = new Random();
            duhaniDom = duhaniDom.OrderBy(x => rand.Next()).ToList();
            varDoms["duhani"] = duhaniDom;

            List<String> pijaDom = new List<String>();
            pijaDom.Add("portokall");
            pijaDom.Add("çaj");
            pijaDom.Add("uje");
            pijaDom.Add("kafe");
            pijaDom.Add("qumesht");
            // shufle pija domain
            rand = new Random();
            pijaDom = pijaDom.OrderBy(x => rand.Next()).ToList();
            varDoms["pija"] = pijaDom;

            CSP = Backtrack(CSP, vars, varDoms);
            Console.WriteLine("Zgjidhja e Zebra Riddle me Backtracking");
            Console.WriteLine("Lenda: Inteligjenca Artificiale");
            Console.WriteLine("Ligjeruar nga Dr. Msc. Nysret Musliu dhe Msc. Arbnor Halili");
            Console.WriteLine("Punuar nga studentet: Nora Ibrahimi dhe Labinot Vila");
            Console.WriteLine("\n");
            Answer(CSP);
            Console.WriteLine("Gjendjet totale: " + states);

            Console.ReadKey();
        }
    }
}

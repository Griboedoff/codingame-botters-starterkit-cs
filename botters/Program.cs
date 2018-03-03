using System;
using System.Collections.Generic;

namespace botters
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var reader = new StateReader();
            var initData = reader.ReadInitData();

            var ironManAi = new KiteAi(initData.AttackDirection, HeroType.IronMan);
            var doctorStrangeAi = new KiteAi(initData.AttackDirection, HeroType.DoctorStrange);
            var ais = new List<AiBase>() {ironManAi, doctorStrangeAi,};

            while (true)
            {
                var state = reader.ReadState(initData);
                var countdown = new Countdown(45);
                if (state.RoundType == -2)
                {
                    Console.WriteLine(CommandHelper.SelectHero(HeroType.IronMan));
                    continue;
                }

                if (state.RoundType == -1)
                {
                    Console.WriteLine(CommandHelper.SelectHero(HeroType.DoctorStrange));
                    continue;
                }

                foreach (var ai in ais)
                {
                    var move = ai.GetNextMove(state, countdown);
                    Console.Error.WriteLine(countdown);
                    Console.WriteLine(move);
                }
            }
        }
    }
}
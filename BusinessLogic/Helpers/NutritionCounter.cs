using KFitServer.DBContext.Models;

namespace KFitServer.Helpers
{
    public class NutritionCounter
    {
        public int Calories { get; private set; }

        public int Proteins { get; private set; }

        public int Lipids { get; private set; }

        public int Carbohydrates { get; private set; }

        public double Water { get; private set; }

        public UserPersonalStatistic UserPersonalStatistic {get;private set;}
       /* public UsersPersonalParameter UsersPersonalParameter { get; private set; }


        public NutritionCounter(UsersPersonalParameter usersPersonalParameter, UserPersonalStatistic userPersonalStatistic) 
        { 
            UsersPersonalParameter = usersPersonalParameter;
            UserPersonalStatistic = userPersonalStatistic;
            Calories = (int)CountNormOfCalories();
            Proteins = (int)CountProteins();
            Lipids = (int) CountLipids();
            Carbohydrates = (int) CountCarbohidrates();
            Water = CountWater();
        }*/
        //private uint CountNormOfCalories()

        /*{
            DateTime? dateOfBirth = UsersPersonalParameter.UserDateOfBirth?.ToDateTime(TimeOnly.Parse("00:00 PM"));
            DateTime? currentDate = DateTime.Now;
            TimeSpan? difference = currentDate - dateOfBirth;

            double ageInYears = (double)(difference?.Days / 365.25);
            double caloriesNorm = 0.0;
            if (UsersPersonalParameter.UserGender == "Ж")
            {
                caloriesNorm = ((655.0 + 9.6 * Convert.ToDouble(UserPersonalStatistic.UserWeight) + 1.8 *
                    Convert.ToDouble(UsersPersonalParameter.UserHeight) - 4.7 * ageInYears) * 1.375);
            }
            else
            {
                caloriesNorm = ((66.0 + 13.75 * Convert.ToDouble(UserPersonalStatistic.UserWeight) + 5 *
                    Convert.ToDouble(UsersPersonalParameter.UserHeight) - 6.75 * ageInYears) * 1.375);
            }

            if (UsersPersonalParameter.UserTarget == 1)
            {
                caloriesNorm = caloriesNorm - caloriesNorm*0.15;
            }
            else
            {
                caloriesNorm = caloriesNorm + caloriesNorm * 0.15;
            }

            if (caloriesNorm < 1300)
            {
                caloriesNorm = 1300;
            }

            return (uint)caloriesNorm;
        }

        private uint CountCarbohidrates()
        {
            uint result =(uint) (Calories * 0.5 / 4);
            return result;
        }

        private uint CountProteins()
        {
            uint result = (uint)(Calories * 0.3 / 4);
            return result;
        }

        private uint CountLipids()
        {
            uint result = (uint)(Calories * 0.2 / 9);
            return result;
        }

        private double CountWater()
        {
            double result = Convert.ToDouble(UserPersonalStatistic.UserWeight)*0.03;
            return result;
        }*/
    }
}

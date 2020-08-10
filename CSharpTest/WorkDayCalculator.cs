using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            HashSet<DateTime> daysOff = default;
            if (weekEnds != null && weekEnds.Length > 0)
            {
                daysOff = GetDaysOffDates(weekEnds);
            }
            DateTime tempDate = startDate;
            for (int i = 1; i < dayCount; i++)
            {
                if (daysOff != default && daysOff.Contains(tempDate))
                {
                    dayCount++;
                }
                tempDate = IncrementDate(tempDate);
            }

            return tempDate;
        }
        private HashSet<DateTime> GetDaysOffDates(WeekEnd[] weekEnds)
        {
            HashSet<DateTime> daysOff = new HashSet<DateTime>();
            foreach (var date in weekEnds)
            {
                DateTime tempDate = date.StartDate;
                do
                {
                    if (!daysOff.Contains(tempDate))
                    {
                        daysOff.Add(tempDate);
                    }
                    tempDate = tempDate.AddDays(1);
                }
                while (tempDate <= date.EndDate);
            }
            return daysOff;
        }
        private DateTime IncrementDate(DateTime date) => date.AddDays(1);
    }
}

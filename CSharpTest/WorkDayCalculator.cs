using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        /// <summary>
        /// Get latest vacation workday excluding weekends and holiday.
        /// </summary>
        /// <param name="startDate">date to start from</param>
        /// <param name="dayCount">day of vacation</param>
        /// <param name="weekEnds"></param>
        /// <returns></returns>
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            IEnumerable<DateTime> daysOff = default;
            if (weekEnds != null && weekEnds.Length > 0)
            {
                daysOff = GetDaysOffDates(weekEnds);
            }

            DateTime tempDate = ValidateDate(daysOff, startDate);
            for (int i = 1; i < dayCount; i++)
            {
                if (daysOff != default && daysOff.Contains(tempDate))
                {
                    dayCount++;
                }

                tempDate = IncrementDate(tempDate);

            }

            return ValidateDate(daysOff,tempDate);
        }
       /// <summary>
       /// Convert weekend date to DateTime set. 
       /// </summary>
       /// <param name="weekEnds"></param>
       /// <returns>set of dates in DateTime format</returns>
        private IEnumerable<DateTime> GetDaysOffDates(WeekEnd[] weekEnds)
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
        /// <summary>
        /// increment date by 1 day
        /// </summary>
        /// <param name="date">increment date</param>
        /// <returns>next date</returns>
        private DateTime IncrementDate(DateTime date) => date.AddDays(1);
        /// <summary>
        /// Check if the date not goes to weekend
        /// </summary>
        /// <param name="daysOff">weekneds HashSet</param>
        /// <param name="date"> date to validate </param>
        /// <returns>closest workday to "date"</returns>
        private DateTime ValidateDate(IEnumerable<DateTime> daysOff, DateTime date)
        {
            if (daysOff != default)
            {
                DateTime tempDate = date;
                while (daysOff.Contains(tempDate))// get nearest workday 
                {
                    tempDate = IncrementDate(tempDate); 
                }
                return tempDate;
            }
            return date;

        }
    }
}

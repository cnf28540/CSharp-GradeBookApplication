using GradeBook.Enums;
using GradeBook.GradeUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {

        List<char> possibleGrades;
        private GradeRanker ranker;

        public RankedGradeBook(string name) : base(name)
        {
            possibleGrades = new List<char>
            {
                'A',
                'B',
                'C',
                'D',
                'F'
            };

            Type = GradeBookType.Ranked;
        }

        public void initializeRanker(List<Student> students, List<char> possibleGrades)
        {
            if(ranker == null)
            {
                ranker = new GradeRanker(students, possibleGrades);
            }
        }

        public override void CalculateStatistics()
        {
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            base.CalculateStudentStatistics(name);
        }

        public override double GetGPA(char letterGrade, StudentType studentType)
        {
            return base.GetGPA(letterGrade, studentType);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            initializeRanker(Students, possibleGrades);

            return ranker.getRanking(averageGrade) ;
        }
    }
}

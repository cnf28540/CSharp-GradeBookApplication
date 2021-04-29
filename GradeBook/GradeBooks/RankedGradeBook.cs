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
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {

            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

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

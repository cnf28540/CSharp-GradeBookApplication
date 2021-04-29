using System;
using System.Collections.Generic;
using System.Text;
using GradeBook;
using GradeBook.Enums;
using GradeBook.GradeBooks;
using Xunit;

namespace GradeBookTests
{
    public class CodysTests
    {

        public Student generateStudent(double average)
        {
            Student output = new Student("TEST",StudentType.Standard,EnrollmentType.Campus);

            output.AddGrade(average);

            return output;
        }

        public void populateGradeBook(BaseGradeBook book, double upperGrade, double stepSize)
        {
            double grade = upperGrade;

            while (grade > 0)
            {
                book.AddStudent(generateStudent(grade));
                grade = grade - stepSize;
            }
        }

        [Fact]
        public void checkRankedGradeBookMeetsRequirements()
        {
            var gradebook = new RankedGradeBook("testBook", false);

            populateGradeBook(gradebook, 100.0, 20.0);

            // This will result in
            // 100, 80, 60, 40 20

            // Top 20 = x >= 80
            // 20-40  = 80 > x >= 60
            // 40-60  = 60 > x >= 40
            // 60-80  = 40 > x >= 20
            // else F

            Assert.Equal('A', gradebook.GetLetterGrade(100.0));
            //Assert.Equal('A', gradebook.GetLetterGrade(80.1));
            //Assert.Equal('A', gradebook.GetLetterGrade(80.0));
            //Assert.Equal('B', gradebook.GetLetterGrade(79.9));
            //Assert.Equal('B', gradebook.GetLetterGrade(60.1));
            //Assert.Equal('B', gradebook.GetLetterGrade(60.0));
            //Assert.Equal('C', gradebook.GetLetterGrade(59.9));
            //Assert.Equal('C', gradebook.GetLetterGrade(40.1));
            //Assert.Equal('C', gradebook.GetLetterGrade(40.0));
            //Assert.Equal('D', gradebook.GetLetterGrade(20.1));
            //Assert.Equal('D', gradebook.GetLetterGrade(20.0));
            //Assert.Equal('F', gradebook.GetLetterGrade(0.0));

        }

    }
}

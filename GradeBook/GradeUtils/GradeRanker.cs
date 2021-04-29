using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeUtils
{
    class GradeRanker
    {

        private List<double> gradeBoundaries;
        private List<char> gradeMapping;

        public GradeRanker(List<Student> studentRecords, List<char> mapping)
        {

            gradeBoundaries = new List<double>();
            gradeMapping = new List<char>();

            // Max ... a ... b ... c ... Min

            // Sort the student averages
            List<double> averages = new List<double>();

            foreach(Student student in studentRecords)
            {
                averages.Add(student.AverageGrade);
            }

            averages.Sort();
            averages.Reverse();

            // Divide the input space into mapping.Count subdivisons.

            int totalStudents = studentRecords.Count;
            int totalGrades = mapping.Count;
            int areaSize = studentRecords.Count / mapping.Count;


            if(totalGrades == 0)
            {
                gradeBoundaries.Add(0);
                gradeMapping.Add('A');
            }

            if(totalGrades == 1)
            {
                gradeBoundaries.Add(averages[averages.Count - 1]);
                gradeMapping.Add(mapping[0]);
            }

            if(totalGrades > 1)
            {
                for (int i = 1; i < studentRecords.Count; i = i + areaSize)
                {
                    gradeBoundaries.Add(averages[areaSize * i]);
                    gradeMapping.Add(mapping[i - 1]);
                }
            }

            // And add an entry for anything less than the minimum

            gradeBoundaries.Add(Double.MinValue);
            gradeMapping.Add(mapping[mapping.Count - 1]);



        }

        public char getRanking(double input)
        {
            int index = 0;

            foreach (double bound in gradeBoundaries)
            {
                if (input > bound)
                {
                    return gradeMapping[index];
                }

                index++;
            }

            // shouldn't hit here...
            return 'F';
        }
 

    }
}

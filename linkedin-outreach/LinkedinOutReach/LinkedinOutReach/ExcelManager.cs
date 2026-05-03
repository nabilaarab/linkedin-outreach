using LinkedinOutReach.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class ExcelManager
    {
        public LinkedinProfile[] LinkedinProfiles;

        public void loadLinkedinProfiles()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input", "linkedin_profiles.csv");

            string[] lines = File.ReadAllLines(filePath);

            this.LinkedinProfiles = new LinkedinProfile[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(',');

                LinkedinProfile linkedinProfile = new LinkedinProfile
                (
                    columns[0],
                    columns[1],
                    columns[2],
                    columns[3]
                );
                this.LinkedinProfiles[i] = linkedinProfile;
            }
        }

        public void uploadLinkedinProfiles(List<LinkedinProfile> linkedinProfiles)
        {
            string fileName = $"linkedin_profiles_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output", fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (LinkedinProfile linkedinProfile in linkedinProfiles)
                {
                    string line = $"{linkedinProfile.Id},{linkedinProfile.Link},{linkedinProfile.Name},{linkedinProfile.CompanyName}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}

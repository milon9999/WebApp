using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modules.WebApp.Model;

namespace Modules.WebApp
{
    public class FileImporter 
    {
        public readonly Dictionary<DateTime, List<CurrencyRate>> Dates = new Dictionary<DateTime, List<CurrencyRate>>();
        private readonly List<string> _ext = new List<string> { "csv" };
        public FileImporter(string dir,string daysShowCount)
        {
            //Taking all csv files 
            var importedFiles = Directory
                .EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
                .Where(s => _ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()));
            

            var listFileNames = new List<FileInfo>();

            foreach (var importedFile in importedFiles)
            {
                listFileNames.Add( new FileInfo(importedFile));
            }

            if (listFileNames.Any())
            {
                //Taking last file by created date if there will be a lot of import files.
                StreamReader file = GetLastCreatedFile(listFileNames);

                //Collect data from file line by line
                DataBind(file, Dates, daysShowCount);
            }
            else
            {
                Dates = new Dictionary<DateTime, List<CurrencyRate>>();
            }
        }

        private StreamReader GetLastCreatedFile(List<FileInfo> listFileNames)
        {
            FileInfo file = null;
            DateTime lastFileCreated = DateTime.MinValue;
            foreach (var fileInfo in listFileNames)
            {
                if (fileInfo.CreationTime > lastFileCreated)
                {
                    lastFileCreated = fileInfo.CreationTime;
                    file = fileInfo;
                }
            }
            return new StreamReader(file.FullName);
        }

        private void DataBind(StreamReader file, Dictionary<DateTime, List<CurrencyRate>> dates, string daysShowCount)
        {
            //Max count days to show in combobox
            int defaultMaxCountDays = 20;
            if (!string.IsNullOrEmpty(daysShowCount) && int.TryParse(daysShowCount, out var daysShowCountConfig))
                defaultMaxCountDays = daysShowCountConfig;

            var dayCount = 0;
            var rates = new List<string>();
            using (StreamReader sr = file)
            {
                var readFirstLine = false;
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (dayCount >= defaultMaxCountDays)
                        continue;
                    
                    if (!readFirstLine)
                    {
                        var line = currentLine.Split(',');
                        foreach (var val in line)
                        {
                            if (string.IsNullOrEmpty(val) || val == line.FirstOrDefault())
                                continue;
                            
                            rates.Add(val);
                        }

                        readFirstLine = true;
                    }
                    else
                    {
                        var collectRates = new List<CurrencyRate>();
                        var line = currentLine.Split(',');
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (double.TryParse(line[i], out var rateValue))
                            {
                                collectRates.Add(new CurrencyRate{Name = rates[i-1], Value = rateValue});
                            }
                            else if (line[i].ToLower() == "n/a")
                            {
                                collectRates.Add(new CurrencyRate { Name = rates[i - 1], Value = 0 });
                            }
                        }
                        dates.Add(Convert.ToDateTime(line[0]), collectRates);
                        dayCount++;
                    }
                }
            }
        }
    }
}

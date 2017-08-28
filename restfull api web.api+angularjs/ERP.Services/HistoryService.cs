using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.HistoryStuff;
using ERP.Core.Models;
using ERP.Core.Repository;
using ERP.Core.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Omu.ValueInjecter;
using Zaybu.Compare;
using Zaybu.Compare.Types;

namespace ERP.Services
{
    public class HistoryService: CrudService<HistoryEmployee>, IHistoryService
    {
        private ICrudService<Employee> _employeeService;

        public HistoryService(IRepo<HistoryEmployee> repo, ICrudService<Employee> employeeService) : base(repo)
        {
            this._employeeService = employeeService;
        }

        public IList<SendingHistoryPoco> GetShortHistoryById(int Id)
        {
            var dataList = Repo.GetAll().Where(h => h.EmployeeId == Id)
                .OrderByDescending(h => h.CreateDate).Take(6).ToList()
                .Select((h) => {
                    var historyPoco = new HistoryPoco().InjectFrom(JsonConvert.DeserializeObject<HistoryPoco>(h.SerializedEmployee)) as HistoryPoco;
                    historyPoco.ModifyBy = h.CreatedBy;
                    historyPoco.ModifyDate = h.CreateDate;
                    return historyPoco;
                })
                .ToList();
            int capacity = 5;
            IList<SendingHistoryPoco> historyToFrontEnd = new List<SendingHistoryPoco>(capacity);
            if (dataList.Count < 2)
                return historyToFrontEnd;

            for (int i = 0; i < dataList.Count() - 1; i++)
            {
                ZCompareResults results = ZCompare.Compare(dataList[i + 1], dataList[i]);

                var positionResult = results.GetResult(dataList[i + 1], "PositionName");
                if (positionResult.Status == ResultStatus.Changed)
                {
                    if (historyToFrontEnd.Count >= capacity)
                        return historyToFrontEnd;
                    else
                        historyToFrontEnd.Add(new SendingHistoryPoco()
                        {
                            ModifyDate = (DateTime)dataList[i].ModifyDate,
                            Performer = dataList[i].ModifyBy,
                            Changes = String.Format("Position changed - from '{0}' to '{1}'", positionResult.OriginalValueAsString, positionResult.ChangedToValueAsString)
                        });
                }

                var skillsResults = results.GetResult(dataList[i + 1].Skills);
                if (skillsResults.Status == ResultStatus.Changed && skillsResults.Children != null)
                {
                    for (int j = skillsResults.Children.Count - 1; j >= 0; j--)
                    {
                        var s = skillsResults.Children[j];
                        string changes = null;
                        switch (s.Status)
                        {
                            case ResultStatus.Changed:
                                changes = string.Format("Skill changed - from '{0}' to '{1}'", s.OriginalValueAsString, s.ChangedToValueAsString);
                                break;
                            case ResultStatus.Added:
                                changes = string.Format("Skill added - {0}", s.ChangedToValueAsString);
                                break;
                            case ResultStatus.Deleted:
                                changes = string.Format("Skill deleted - {0}", s.OriginalValueAsString);
                                break;
                            default:
                                break;
                        }
                        if (changes != null)
                        {
                            if (historyToFrontEnd.Count >= capacity)
                                return historyToFrontEnd;
                            else
                                historyToFrontEnd.Add(new SendingHistoryPoco()
                                {
                                    ModifyDate = (DateTime)dataList[i].ModifyDate,
                                    Performer = dataList[i].ModifyBy,
                                    Changes = changes
                                });
                        }
                    }
                }

                var certificatesResults = results.GetResult(dataList[i + 1].Certificates);
                if (certificatesResults.Status == ResultStatus.Changed && certificatesResults.Children != null)
                {
                    for (int j = certificatesResults.Children.Count - 1; j >= 0; j--)
                    {
                        var c = certificatesResults.Children[j];
                        string changes = null;
                        switch (c.Status)
                        {
                            case ResultStatus.Changed:
                                break;
                            case ResultStatus.Added:
                                changes = string.Format("Certificate added - {0}", c.ChangedToValueAsString);
                                break;
                            case ResultStatus.Deleted:
                                changes = string.Format("Certificate deleted - {0}", c.OriginalValueAsString);
                                break;
                            default:
                                break;
                        }
                        if (changes != null)
                        {
                            if (historyToFrontEnd.Count >= capacity)
                                return historyToFrontEnd;
                            else
                                historyToFrontEnd.Add(new SendingHistoryPoco()
                                {
                                    ModifyDate = (DateTime)dataList[i].ModifyDate,
                                    Performer = dataList[i].ModifyBy,
                                    Changes = changes
                                });
                        }
                    }
                }
            }

            return historyToFrontEnd;
        }

        public IList<SendingHistoryPoco> GetHistoryById(int Id)
        {
             var dataList = Repo.GetAll().Where(h => h.EmployeeId == Id)
                .OrderByDescending(h => h.CreateDate).ToList()
                .Select((h) => {
                    var historyPoco = new HistoryPoco().InjectFrom(JsonConvert.DeserializeObject<HistoryPoco>(h.SerializedEmployee)) as HistoryPoco;
                    historyPoco.ModifyBy = h.ModifyBy;
                    historyPoco.ModifyDate = h.CreateDate;
                    return historyPoco;
                }) 
                .ToList();
            IList<SendingHistoryPoco> historyToFrontEnd = new List<SendingHistoryPoco>();
            if (dataList.Count < 2)
                return historyToFrontEnd;

            for (int i = 0; i < dataList.Count() - 1; i++)
            {
                ZCompareResults results = ZCompare.Compare(dataList[i + 1], dataList[i]);

                var positionResult = results.GetResult(dataList[i + 1], "PositionName");
                if (positionResult.Status == ResultStatus.Changed)
                {
                    historyToFrontEnd.Add(new SendingHistoryPoco()
                    {
                        ModifyDate = (DateTime)dataList[i].ModifyDate,
                        Performer = dataList[i].ModifyBy,
                        Changes = String.Format("Position changed - from '{0}' to '{1}'", positionResult.OriginalValueAsString, positionResult.ChangedToValueAsString)
                    });
                }

                var skillsResults = results.GetResult(dataList[i + 1].Skills);
                if (skillsResults.Status == ResultStatus.Changed && skillsResults.Children != null)
                {
                    for (int j = skillsResults.Children.Count - 1; j >= 0; j--)
                    {
                        var s = skillsResults.Children[j];
                        string changes = null;
                        switch (s.Status)
                        {
                            case ResultStatus.Changed:
                                changes = string.Format("Skill changed - from '{0}' to '{1}'", s.OriginalValueAsString, s.ChangedToValueAsString);
                                break;
                            case ResultStatus.Added:
                                changes = string.Format("Skill added - {0}", s.ChangedToValueAsString);
                                break;
                            case ResultStatus.Deleted:
                                changes = string.Format("Skill deleted - {0}", s.OriginalValueAsString);
                                break;
                            default:
                                break;
                        }
                        if (changes != null)
                        {
                            historyToFrontEnd.Add(new SendingHistoryPoco()
                            {
                                ModifyDate = (DateTime)dataList[i].ModifyDate,
                                Performer = dataList[i].ModifyBy,
                                Changes = changes
                            });
                        }
                    }
                }

                var certificatesResults = results.GetResult(dataList[i + 1].Certificates);
                if (certificatesResults.Status == ResultStatus.Changed && certificatesResults.Children != null)
                {
                    for (int j = certificatesResults.Children.Count - 1; j >= 0; j--)
                    {
                        var c = certificatesResults.Children[j];
                        string changes = null;
                        switch (c.Status)
                        {
                            case ResultStatus.Changed:
                                break;
                            case ResultStatus.Added:
                                changes = string.Format("Certificate added - {0}", c.ChangedToValueAsString);
                                break;
                            case ResultStatus.Deleted:
                                changes = string.Format("Certificate deleted - {0}", c.OriginalValueAsString);
                                break;
                            default:
                                break;
                        }
                        if (changes != null)
                        {
                            historyToFrontEnd.Add(new SendingHistoryPoco()
                            {
                                ModifyDate = (DateTime)dataList[i].ModifyDate,
                                Performer = dataList[i].ModifyBy,
                                Changes = changes
                            });
                        }
                    }
                }
            }

            return historyToFrontEnd;
        }

        public virtual int Update(int employeeId)
        {
            var newHistoryPoco = this._employeeService.Get(employeeId).MapToEmployeeHistory();
            if (newHistoryPoco == null)
                return 0;

            var lastEmployeeHistory = Repo.GetAll().Where(h => h.EmployeeId == employeeId).ToList()
                .OrderByDescending(h => h.CreateDate)
                .FirstOrDefault();

            if (lastEmployeeHistory != null)
            {
                var currentHistoryPoco = JsonConvert.DeserializeObject<HistoryPoco>(lastEmployeeHistory.SerializedEmployee) as HistoryPoco;
                var results = ZCompare.Compare<HistoryPoco>(currentHistoryPoco, newHistoryPoco);
                if (results.Identical)
                    return 0;
            }

            return Update(new HistoryEmployee()
            {
                SerializedEmployee = JsonConvert.SerializeObject(newHistoryPoco),
                EmployeeId = employeeId
            });
        }
    }
}

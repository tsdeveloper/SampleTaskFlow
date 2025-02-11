using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
namespace Core.Entities;


public enum EAuditType
{
    None = 0,
    Create = 1,
    Update = 2,
    Delete = 3
}
    [ExcludeFromCodeCoverage]

public class Audit
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public EAuditType Type { get; set; }
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
    public string AffectedColumns { get; set; }
    public string PrimaryKey { get; set; }
}
     
    [ExcludeFromCodeCoverage]
public class AuditEntry 
{
    public AuditEntry(EntityEntry entry) => Entry = entry;

    public EntityEntry Entry { get; }
    public string UserId { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object> KeyValues { get; } = new();
    public Dictionary<string, object> OldValues { get; } = new();
    public Dictionary<string, object> NewValues { get; } = new();
    public EAuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new();

    public Audit ToAudit()
    {
        var audit = new Audit
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            Type = AuditType,
            TableName = TableName,
            DateTime = DateTime.Now,
            PrimaryKey = JsonConvert.SerializeObject(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns)
        };

        return audit;
    }
}

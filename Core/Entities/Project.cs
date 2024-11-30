using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
  public enum EProjectStatusType
  {
    NOSTARTING,
    STARTING,
    COMPLETED,
    CANCELED,
  }
      [ExcludeFromCodeCoverage]

  public class Project : BaseEntity
  {
    public Project()
    {

    }
    public Project(int projectId)
    {
      ProjectId = projectId;
    }
    [Key]
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int ProjectUserId { get; set; }
    public EProjectStatusType ProjectStatus { get; set; }
    public DateTime? ProjectCompletedAt { get; set; }
    public int ProjectMaxLimitTask { get; set; }
    public ICollection<Task> TaskList { get; set; } = new List<Task>();

  }

}

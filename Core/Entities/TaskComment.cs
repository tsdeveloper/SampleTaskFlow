namespace Core.Entities
{
  public class TaskComment : BaseEntity
  {
    public TaskComment()
    {

    }
    public TaskComment(int taskCommentId)
    {
      TaskCommentId = taskCommentId;
    }
    public int TaskCommentId { get; set; }
    public int TaskId { get; set; }
    public string TaskCommentDescription { get; set; }
    public Task Task { get; set; }

  }
}

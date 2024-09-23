using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public string taskName;  // The name of the task associated with this trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player triggered the zone
        {
            TaskManager taskManager = other.GetComponent<TaskManager>();
            if (taskManager != null)
            {
                taskManager.CompleteTask(taskName);  // Notify the task manager which task was completed
            }
        }
    }
}

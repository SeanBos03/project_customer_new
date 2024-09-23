using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // List of tasks (could be GameObjects, Enums, or Task IDs)
    private List<string> taskOrder = new List<string>() { "Task1", "Task2", "Task3" };

    // Tracks the current task that should be completed
    private int currentTaskIndex = 0;

    // Reference to the character or player who will be doing tasks
    public GameObject player;

    // To keep track of whether a penalty should be applied
    public bool isPunished = false;

    void Start()
    {
        // Initialize the task sequence
        currentTaskIndex = 0;
        isPunished = false;
    }

    // Function to call when the player completes a task
    public void CompleteTask(string taskName)
    {
        if (currentTaskIndex >= taskOrder.Count)
        {
            Debug.Log("All tasks are completed!");
            return;
        }

        if (taskOrder[currentTaskIndex] == taskName)
        {
            Debug.Log("Task completed in the correct order: " + taskName);
            // Move to the next task
            currentTaskIndex++;
        }
        else
        {
            Debug.Log("Task completed out of order! Penalty applied.");
            ApplyPunishment();
        }
    }

    // Function to apply the punishment
    void ApplyPunishment()
    {
        isPunished = true;
        player.GetComponent<Player>().ApplyPenalty();
    }

    // Optionally, you could check if all tasks are done
    public bool AreAllTasksCompleted()
    {
        return currentTaskIndex >= taskOrder.Count;
    }
}

public class Player : MonoBehaviour
{
    public int health = 100;
    
    public void ApplyPenalty()
    {
        Debug.Log("Penalty applied: Reducing health.");
        health -= 10;  
    }
}

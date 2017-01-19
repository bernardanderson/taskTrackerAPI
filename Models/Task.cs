using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace taskTracker.Models
{
  public class UserTask
  {
    [Required]
    [Key]
    public int TaskId {get;set;}

    [Required]
    public string Name {get;set;}

    public string Description {get;set;}

    public enum Status {ToDo, InProgress, Complete}
    
    [Required]
    public Status TaskStatus {get;set;}

    [DataType(DataType.DateTime)]
    public DateTimeOffset CompletedOn {get;set;}

    }
}
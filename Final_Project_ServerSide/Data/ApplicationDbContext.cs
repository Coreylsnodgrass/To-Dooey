using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<ToDoList> ToDoLists { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Infrastructure.Persistence.Configuration;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    // TODO: Rip out the common pieces that are from BaseEntity (https://github.com/SSWConsulting/Cafe365.Melbourne/issues/78)
    // virtual method, override 
    // Good marker to enforce that all entities have configuration defined via arch tests
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new TodoItemId(x))
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
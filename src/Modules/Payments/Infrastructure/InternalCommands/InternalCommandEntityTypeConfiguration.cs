﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
{
    public void Configure(EntityTypeBuilder<InternalCommand> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Processed)
            .HasDefaultValue(false);
    }
}

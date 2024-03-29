﻿namespace Firebase
{
    public class Shoe
    {
        public string Id { get; set; } = string.Empty;

        public required string Name { get; set; }

        public required string Brand { get; set; }

        public required decimal Price { get; set; }

        public Uri VideoUri { get; set; } = default!;
    }
}

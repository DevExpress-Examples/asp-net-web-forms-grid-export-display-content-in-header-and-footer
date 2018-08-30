using System;
using System.Collections.Generic;

[Serializable]
public class Invoice : IEquatable<Invoice> {
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Invoice(int id, string description, decimal price) {
        Id = id;
        Description = description;
        Price = price;
    }

    public Invoice() { }

    public bool Equals(Invoice other) {
        return this.Id.Equals(other.Id);
    }
}
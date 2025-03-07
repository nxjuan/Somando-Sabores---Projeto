package com.github.restaurante.domain.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table
@Builder
public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private String id;

    @Column(name="name", length = 60, nullable = false, unique = true)
    private String name;

    @Column(name="price", nullable = false)
    private Double price;

    @Column
    private String descriptions;

    @JsonIgnore
    @ManyToMany(mappedBy = "products")
    private List<Sale> sales;
}

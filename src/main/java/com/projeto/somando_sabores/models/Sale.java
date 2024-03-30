package com.projeto.somando_sabores.models;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;

import java.io.Serializable;
import java.time.Instant;

@Entity
@Table(name = "sales")
public class Sale implements Serializable{
    @Id
    @Column(name="id", unique = true)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;
    @Column(name="date")
    @NotBlank
    private Instant date;
    @Column(name="value")
    @NotBlank
    private double value;

    @ManyToOne
    @JoinColumn(name="user_id", nullable = false, updatable = false)
    private User user;
}

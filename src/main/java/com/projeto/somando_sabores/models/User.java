package com.projeto.somando_sabores.models;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;


@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name = User.TABLE_NAME)
public class User implements Serializable {

    public static final String TABLE_NAME = "user";

    @Id
    @Column(name = "id", unique = true)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;


    @Column(name = "name", length = 100, nullable = false)
    @NotBlank
    @Size(min = 1, max = 255)
    private String name;

    @Column(name = "email", length = 50, nullable = false, unique = true)
    @NotBlank
    @Size(min = 1, max = 255)
    private String email;

    @Column(name = "cpf", length = 11, nullable = false, unique = true)
    private String cpf;

    @OneToMany(mappedBy = "user")
    private List<Sale> sales;

}

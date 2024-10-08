package com.projeto.somando_sabores.models;

import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name = Product.TABLE_NAME)
public class Product implements Serializable {

    public static final String TABLE_NAME = "product";

    @Id
    @Column(name = "id", unique = true)
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name="name", length = 60, nullable = false, unique = true)
    @NotBlank
    private String name;

    @Column(name="price", nullable = false)
    private Double price;

    @ElementCollection
    @CollectionTable(
            name = "product_description",
            joinColumns = @JoinColumn(name = "product_id")
    )
    @Column(name="description")
    List<String> descriptions = new ArrayList<>();

    @JsonIgnore
    @ManyToMany(mappedBy = "products")
    private List<Sale> sales;

}

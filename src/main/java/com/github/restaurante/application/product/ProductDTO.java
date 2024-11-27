package com.github.restaurante.application.product;

import com.github.restaurante.domain.entities.Sale;
import lombok.Data;

import java.util.List;

@Data
public class ProductDTO {
    private String id;
    private String name;
    private Double price;
    private String descriptions;
    private List<Sale> sales;
}

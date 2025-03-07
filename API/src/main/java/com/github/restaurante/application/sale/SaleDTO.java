package com.github.restaurante.application.sale;

import com.github.restaurante.domain.entities.Product;
import lombok.Data;

import java.time.Instant;
import java.util.List;

@Data
public class SaleDTO {
    private String id;
    private Instant date;
    private Double value;
    private String customer;
    private List<Product> products;
}

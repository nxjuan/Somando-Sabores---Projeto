package com.github.restaurante.domain.service;

import com.github.restaurante.domain.entities.Product;

import java.util.List;

public interface ProductService {

    public Product findById(String id);

    public List<Product> findAll();

    public Product create(Product product);

    public Product update(Product product);

    public void delete(String id);

}

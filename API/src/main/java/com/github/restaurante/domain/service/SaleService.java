package com.github.restaurante.domain.service;

import com.github.restaurante.domain.entities.Sale;

import java.util.List;

public interface SaleService {
    public Sale findById(String id);

    public List<Sale> findAll();

    public Sale create(Sale sale);

    public Sale update(Sale sale);



}

package com.github.restaurante.application.sale;

import com.github.restaurante.domain.entities.Sale;
import org.springframework.stereotype.Component;

@Component
public class SaleMapper {
    public Sale mapToSale(SaleDTO dto){
        return Sale.builder()
                .date(dto.getDate())
                .value(dto.getValue())
                .customer(dto.getCustomer())
                .products(dto.getProducts())
                .build();
    }
}

package com.github.restaurante.application.product;

import com.github.restaurante.domain.entities.Product;
import org.springframework.stereotype.Component;

@Component
public class ProductMapper {
    public Product mapToProduct(ProductDTO dto){
        return Product.builder()
                .name(dto.getName())
                .price(dto.getPrice())
                .descriptions(dto.getDescriptions())
                .build();
    }
}

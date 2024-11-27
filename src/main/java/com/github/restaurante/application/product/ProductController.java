package com.github.restaurante.application.product;

import com.github.restaurante.domain.entities.Product;
import com.github.restaurante.domain.service.ProductService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/product")
@RequiredArgsConstructor
public class ProductController {

    private final ProductMapper productMapper;
    private final ProductService productService;

    @GetMapping("/getById/{id}")
    public ResponseEntity<Product> findById(@PathVariable String id){
        return ResponseEntity.ok(this.productService.findById(id));
    }

    @GetMapping
    public ResponseEntity<List<Product>> findAll() {
        return ResponseEntity.ok(this.productService.findAll());
    }

    @PostMapping
    public ResponseEntity<Void> create(@RequestBody ProductDTO dto){
        Product product = productMapper.mapToProduct(dto);
        productService.create(product);

        return ResponseEntity.status(HttpStatus.CREATED).build();
    }

    @PutMapping("/update/{id}")
    public ResponseEntity<Void> update(@RequestBody ProductDTO dto, @PathVariable String id){
        Product productToUpdate = productMapper.mapToProduct(dto);
        productToUpdate.setId(id);

        Product updatedProduct = productService.update(productToUpdate);

        return ResponseEntity.ok().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable String id){
        this.productService.delete(id);
        return ResponseEntity.noContent().build();
    }

}

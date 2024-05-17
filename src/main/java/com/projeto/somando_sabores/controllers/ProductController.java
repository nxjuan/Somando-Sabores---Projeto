package com.projeto.somando_sabores.controllers;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.services.ProductService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

import java.net.URI;
import java.util.List;

@RestController
@RequestMapping("/product")
@Validated
public class ProductController {

    @Autowired
    private ProductService productService;

    @GetMapping("/{id}")
    public ResponseEntity<Product> findById(@PathVariable Long id){
        Product product = this.productService.findById(id);
        return ResponseEntity.ok(product);
    }

    @GetMapping
    public ResponseEntity<List<Product>> findAll() {
        List<Product> products = this.productService.findAll();
        return ResponseEntity.ok(products);
    }

    @PostMapping
    public ResponseEntity<Void> create(@Valid @RequestBody Product product){
        this.productService.create(product);
        URI uri = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(product.getId()).toUri();
        return ResponseEntity.created(uri).build();
    }

    @PutMapping("/{id}")
    public ResponseEntity<Void> update(@Valid @RequestBody Product product, @PathVariable Long id){
        product.setId(id);
        this.productService.update(product);
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable Long id){
        this.productService.delete(id);
        return ResponseEntity.noContent().build();
    }

}

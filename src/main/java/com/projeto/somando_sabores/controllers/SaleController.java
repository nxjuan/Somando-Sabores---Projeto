package com.projeto.somando_sabores.controllers;

import com.projeto.somando_sabores.models.Sale;
import com.projeto.somando_sabores.services.SaleService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

import java.net.URI;

@RestController
@RequestMapping("/sale")
@Validated
public class SaleController {

    @Autowired
    private SaleService saleService;

    @GetMapping("/{id}")
    public ResponseEntity<Sale> findById(@PathVariable Long id){
        Sale sale = this.saleService.findById(id);
        return ResponseEntity.ok(sale);
    }

    @PostMapping
    @Validated
    public ResponseEntity<Void> create(@Valid @RequestBody Sale sale){
        this.saleService.create(sale);
        URI uri = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}").buildAndExpand(sale.getId()).toUri();
        return ResponseEntity.created(uri).build();
    }

    @PutMapping("/{id}")
    @Validated
    public ResponseEntity<Void> update(@Valid @RequestBody Sale sale, @PathVariable Long id){
        sale.setId(id);
        this.saleService.update(sale);
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable Long id){
        this.saleService.delete(id);
        return ResponseEntity.noContent().build();
    }

}
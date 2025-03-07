package com.github.restaurante.application.sale;

import com.github.restaurante.application.product.ProductDTO;
import com.github.restaurante.domain.entities.Product;
import com.github.restaurante.domain.entities.Sale;
import com.github.restaurante.domain.service.SaleService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/sale")
@RequiredArgsConstructor
public class SaleController {
    private final SaleService saleService;
    private final SaleMapper saleMapper;

    @GetMapping("/getById/{id}")
    public ResponseEntity<Sale> findById(@PathVariable String id){
        return ResponseEntity.ok(this.saleService.findById(id));
    }

    @GetMapping
    public ResponseEntity<List<Sale>> findAll() {
        return ResponseEntity.ok(this.saleService.findAll());
    }

    @PostMapping
    public ResponseEntity<Void> create(@RequestBody SaleDTO dto){
        Sale sale = saleMapper.mapToSale(dto);
        saleService.create(sale);

        return ResponseEntity.status(HttpStatus.CREATED).build();
    }

    @PutMapping("/update/{id}")
    public ResponseEntity<Void> update(@RequestBody SaleDTO dto, @PathVariable String id){
        Sale saleToUpdate = saleMapper.mapToSale(dto);
        saleToUpdate.setId(id);

        Sale updatedSale = saleService.update(saleToUpdate);

        return ResponseEntity.ok().build();
    }
}

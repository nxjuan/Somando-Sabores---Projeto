package com.projeto.somando_sabores.services;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.models.Sale;
import com.projeto.somando_sabores.repositories.SaleRepository;
import com.projeto.somando_sabores.services.exceptions.DataBindingViolationException;
import com.projeto.somando_sabores.services.exceptions.ObjectNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.Instant;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class SaleService {

    @Autowired
    private SaleRepository saleRepository;

    @Autowired
    private ProductService productService;


    public Sale findById(Long id){
        Optional<Sale> sale = this.saleRepository.findById(id);
        return sale.orElseThrow(
            () -> new ObjectNotFoundException(
                "Venda não encontrada! id: " + id + ", tipo: " + Sale.class.getName()
            )
        );
    }

    public List<Sale> findAllById(List<Long> ids) {
        return saleRepository.findAllById(ids);
    }

    @Transactional
    public Sale create(Sale sale){

        List<Long> productIds = sale.getProducts().stream()
                .map(Product::getId)
                .collect(Collectors.toList());

        double total = productIds.isEmpty() ? 0.0 : productIds.stream()
                .mapToDouble(productId -> productService.findById(productId).getPrice())
                .sum();

        sale.setId(null);
        sale.setDate(Instant.now());

        sale.setValue(total);
//        sale.setValue();
        sale = this.saleRepository.save(sale);
        return sale;
    }

    @Transactional
    public Sale update(Sale sale){
        Sale newSale = findById(sale.getId());
        newSale.setValue(sale.getValue());
        return this.saleRepository.save(newSale);
    }

    public void delete(Long id){
        findById(id);
        try{
            this.saleRepository.deleteById(id);
        }
        catch(Exception e){
            throw new DataBindingViolationException(
                    "Não é possivel excluir pois há entidades relacionadas!"
            );
        }
    }
}

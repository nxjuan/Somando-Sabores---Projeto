package com.projeto.somando_sabores.services;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.models.Sale;
import com.projeto.somando_sabores.models.User;
import com.projeto.somando_sabores.repositories.ProductRepository;
import com.projeto.somando_sabores.repositories.SaleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class SaleService {

    @Autowired
    private SaleRepository saleRepository;

    @Autowired
    private ProductRepository productRepository;


    public Sale findById(Long id){
        Optional<Sale> sale = this.saleRepository.findById(id);
        return sale.orElseThrow(
            () -> new RuntimeException(
                "Venda não encontrada! id: " + id + ", tipo: " + Sale.class.getName()
            )
        );
    }

    public List<Sale> findAllById(List<Long> ids) {
        return saleRepository.findAllById(ids);
    }

    @Transactional
    public Sale create(Sale sale){
        sale.setId(null);
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
            throw new RuntimeException(
                    "Não é possivel excluir pois há entidades relacionadas!"
            );
        }
    }
}

package com.github.restaurante.application.sale;

import com.github.restaurante.domain.entities.Sale;
import com.github.restaurante.domain.exceptions.ObjectNotFoundException;
import com.github.restaurante.domain.service.SaleService;
import com.github.restaurante.infra.repositories.SaleRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class SaleServiceImpl implements SaleService {

    @Autowired
    private SaleRepository saleRepository;

    @Override
    public Sale findById(String id) {
        Optional<Sale> sale = this.saleRepository.findById(id);
        return sale.orElseThrow((() -> new ObjectNotFoundException(
                "Venda não encontrada! id: " + id
        )));
    }

    @Override
    public List<Sale> findAll() {
        return saleRepository.findAll();
    }

    @Override
    public Sale create(Sale sale) {
        return saleRepository.save(sale);
    }

    @Override
    public Sale update(Sale sale) {
        Optional<Sale> newSale = saleRepository.findById(sale.getId());
        if(newSale.isPresent()){
            Sale saleToUpdate = newSale.get();

            return this.saleRepository.save(saleToUpdate);
        }

        throw new ObjectNotFoundException(
                "Venda não encontrada! id: " + sale.getId()
        );
    }



}

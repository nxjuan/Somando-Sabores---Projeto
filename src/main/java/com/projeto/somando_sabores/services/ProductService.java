package com.projeto.somando_sabores.services;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.repositories.ProductRepository;
import com.projeto.somando_sabores.services.exceptions.DataBindingViolationException;
import com.projeto.somando_sabores.services.exceptions.ObjectNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class ProductService {

    @Autowired
    private ProductRepository productRepository;

    public Product findById(Long id){
        Optional<Product> product = this.productRepository.findById(id);
        return product.orElseThrow((() -> new ObjectNotFoundException(
                "Produto não encontrado! id: " + id + ", tipo: " + Product.class.getName()
        )));
    }

    public List<Product> findAll() {
        return productRepository.findAll();
    }

    public List<Product> findAllById(List<Long> ids) {
        return productRepository.findAllById(ids);
    }

    @Transactional
    public Product create(Product product){
        product.setId(null);
        product = this.productRepository.save(product);
        return product;
    }

    @Transactional
    public Product update(Product product){
        Product newProduct = findById(product.getId());
        newProduct.setPrice(product.getPrice());
        return this.productRepository.save(newProduct);
    }

    public void delete(Long id){
        findById(id);
        try{
            this.productRepository.deleteById(id);
        }
        catch (Exception e){
            throw new DataBindingViolationException(
                    "Não é possivel excluir pois há entidades relacionadas!"
            );
        }
    }
}

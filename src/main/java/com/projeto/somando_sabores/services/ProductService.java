package com.projeto.somando_sabores.services;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

@Service
public class ProductService {

    @Autowired
    private ProductRepository productRepository;

    public Product findById(Long id){
        Optional<Product> product = this.productRepository.findById(id);
        return product.orElseThrow((() -> new RuntimeException(
                "Produto não encontrado! id: " + id + ", tipo: " + Product.class.getName()
        )));
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
            throw new RuntimeException(
                    "Não é possivel excluir pois há entidades relacionadas!"
            );
        }
    }
}
package com.github.restaurante.application.product;

import com.github.restaurante.domain.entities.Product;
import com.github.restaurante.domain.exceptions.ObjectNotFoundException;
import com.github.restaurante.domain.service.ProductService;
import com.github.restaurante.infra.repositories.ProductRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class ProductServiceImpl implements ProductService {

    @Autowired
    private ProductRepository productRepository;

    @Override
    public Product findById(String id) {
        Optional<Product> product = this.productRepository.findById(id);
        return product.orElseThrow((() -> new ObjectNotFoundException(
                "Produto não encontrado! id: " + id + ", tipo: " + Product.class.getName()
        )));
    }

    @Override
    public List<Product> findAll() {
        return productRepository.findAll();
    }

    @Override
    public Product create(Product product) {
        return productRepository.save(product);
    }

    @Override
    public Product update(Product product) {
        Optional<Product> newProduct = productRepository.findById(product.getId());
        if(newProduct.isPresent()){

            Product updatedProduct = newProduct.get();

            updatedProduct.setPrice(product.getPrice());
            return this.productRepository.save(updatedProduct);
        }

        throw new ObjectNotFoundException(
                "Produto Não encontrado! id: " + product.getId()
        );

    }

    @Override
    public void delete(String id) {
        Optional<Product> deletedProduct = productRepository.findById(id);

        this.productRepository.deleteById(deletedProduct.get().getId());
    }
}

package com.projeto.somando_sabores.repositories;

import com.projeto.somando_sabores.models.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;



@Repository
public interface ProductRepository extends JpaRepository<Product, Long> {



}

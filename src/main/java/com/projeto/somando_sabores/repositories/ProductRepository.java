package com.projeto.somando_sabores.repositories;

import com.projeto.somando_sabores.models.Product;
import com.projeto.somando_sabores.models.Sale;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ProductRepository extends JpaRepository<Product, Long> {



}

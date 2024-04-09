package com.projeto.somando_sabores.repositories;

import com.projeto.somando_sabores.models.Sale;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface SaleRepository extends JpaRepository<Sale, Long> {

//    List<Sale> findByUser_Id(Long id);
//    List<Sale> findByProduct_Id(Long id);

}

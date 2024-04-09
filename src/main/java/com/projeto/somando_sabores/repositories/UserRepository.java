package com.projeto.somando_sabores.repositories;

import com.projeto.somando_sabores.models.Sale;
import com.projeto.somando_sabores.models.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {



}

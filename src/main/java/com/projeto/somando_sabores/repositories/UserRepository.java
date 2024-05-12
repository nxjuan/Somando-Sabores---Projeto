package com.projeto.somando_sabores.repositories;


import com.projeto.somando_sabores.models.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface UserRepository extends JpaRepository<User, Long> {



}

package com.github.restaurante.domain.service;

import com.github.restaurante.domain.entities.Users;

import java.util.List;

public interface UserService {
    public Users findById(String id);

    public List<Users> findAll();

    public Users create(Users user);

    public Users update(Users user);

    public void delete(String id);

}

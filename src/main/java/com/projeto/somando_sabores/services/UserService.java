package com.projeto.somando_sabores.services;

import com.projeto.somando_sabores.models.User;
import com.projeto.somando_sabores.repositories.UserRepository;
import com.projeto.somando_sabores.services.exceptions.DataBindingViolationException;
import com.projeto.somando_sabores.services.exceptions.ObjectNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;

    public User findById(Long id){
        Optional<User> user = this.userRepository.findById(id);
        return user.orElseThrow((() -> new ObjectNotFoundException(
                "Usuário não encontrado! id: " + id + ", tipo: " + User.class.getName()

        )));
    }

    public List<User> findAll() {
        return this.userRepository.findAll();
    }


    @Transactional
    public User create(User user){
        user.setId(null);
        user = this.userRepository.save(user);
        return user;
    }

    public User update(User user){
        User newUser = findById(user.getId());
        newUser.setName(user.getName());
        return this.userRepository.save(newUser);
    }

    public void delete(Long id){
        findById(id);
        try{
            this.userRepository.deleteById(id);
        }catch (Exception e){
            throw new DataBindingViolationException("Não é possivel excluir pois há entidades relacionadas!");
        }
    }
}

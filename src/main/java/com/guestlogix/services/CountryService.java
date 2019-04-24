package com.guestlogix.services;

import com.guestlogix.database.entities.Country;
import com.guestlogix.database.repositories.interfaces.customs.ICountryRepositoryCustom;
import com.guestlogix.services.interfaces.ICountryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * A class to interface any calls to the Country's repository class.
 *
 * @author Vanderson Assis
 * @since 4/23/2019
 */

@Service
public class CountryService implements ICountryService {
    @Autowired
    private ICountryRepositoryCustom countryRepository;

    @Override
    public Country findByName(String name) {
        return countryRepository.findByName(name);
    }
}

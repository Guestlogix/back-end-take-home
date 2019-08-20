package com.Guestlogix.shortestRoute.config;

import javax.sql.DataSource;

import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.boot.autoconfigure.liquibase.LiquibaseAutoConfiguration;
import org.springframework.boot.autoconfigure.liquibase.LiquibaseProperties;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import liquibase.integration.spring.SpringLiquibase;

/**
 * 
 * @author nchopra
 *
 *
 */
@Configuration
@EnableConfigurationProperties({ LiquibaseProperties.class })
@EnableAutoConfiguration(exclude = LiquibaseAutoConfiguration.class)
public class LiquibaseConfig {
	@ConditionalOnProperty(prefix = "spring.liquibase", name = {"enabled"})
	@Bean
	public SpringLiquibase liquibase(LiquibaseProperties liquibaseProperties, DataSource dataSource) {
		SpringLiquibase liquibase = new SpringLiquibase();
		liquibase.setChangeLog(liquibaseProperties.getChangeLog());
		liquibase.setDataSource(dataSource);
		liquibase.setDropFirst(liquibaseProperties.isDropFirst());
		liquibase.setContexts(liquibaseProperties.getContexts());
		liquibase.setDefaultSchema(liquibaseProperties.getDefaultSchema());
		liquibase.setShouldRun(liquibaseProperties.isEnabled());
		liquibase.setLabels(liquibaseProperties.getLabels());
		liquibase.setChangeLogParameters(liquibaseProperties.getParameters());
		liquibase.setRollbackFile(liquibaseProperties.getRollbackFile());
		return liquibase;
	}
}

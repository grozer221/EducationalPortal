import React from 'react';
import {useMutation} from '@apollo/client';
import {DatePicker, Form, Input, message} from 'antd';
import {useNavigate} from 'react-router-dom';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {CREATE_USER_MUTATION, CreateUserData, CreateUserVars} from '../../users.mutations';
import {Role} from '../../users.types';

type FormValues = {
    firstName: string,
    lastName: string,
    middleName: string,
    login: string,
    password: string,
    email: string,
    phoneNumber: string,
    dateOfBirth: any,
    gradeId: string | null,
}

export const StudentsCreate = () => {
    const [createStudentMutation, createStudentMutationOption] = useMutation<CreateUserData, CreateUserVars>(CREATE_USER_MUTATION);
    const [form] = Form.useForm();

    const navigate = useNavigate();

    const onFinish = async (values: FormValues) => {
        createStudentMutation({
            variables: {
                createUserInputType: {
                    firstName: values.firstName,
                    lastName: values.lastName,
                    middleName: values.middleName,
                    login: values.login,
                    password: values.password,
                    email: values.email,
                    phoneNumber: values.phoneNumber,
                    dateOfBirth: new Date(values.dateOfBirth._d.setHours(12)).toISOString(),
                    role: Role.Student,
                    gradeId: values.gradeId,
                },
            },
        })
            .then(response => {
                navigate('../');
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    return (
        <Form
            name="StudentsCreateForm"
            onFinish={onFinish}
            form={form}
            {...sizeFormItem}
        >
            <Form.Item
                name="firstName"
                label="Ім'я"
                rules={[{required: true, message: 'Введіть Ім\'я!'}]}
            >
                <Input placeholder="Ім'я"/>
            </Form.Item>
            <Form.Item
                name="middleName"
                label="По-батькові"
                rules={[{required: true, message: 'Введіть По-батькові!'}]}
            >
                <Input placeholder="По-батькові"/>
            </Form.Item>
            <Form.Item
                name="lastName"
                label="Прізвище"
                rules={[{required: true, message: 'Введіть Прізвище!'}]}
            >
                <Input placeholder="Прізвище"/>
            </Form.Item>
            <Form.Item
                name="login"
                label="Логін"
                rules={[{required: true, message: 'Введіть Логін!'}]}
            >
                <Input placeholder="Логін"/>
            </Form.Item>
            <Form.Item
                name="password"
                label="Пароль"
                rules={[{required: true, message: 'Введіть Пароль!'}]}
            >
                <Input placeholder="Пароль" type={'password'}/>
            </Form.Item>
            <Form.Item
                name="email"
                label="Email"
                rules={[{type: 'email', message: 'Невірно введено Email!'}]}
            >
                <Input placeholder="Email" type={'email'}/>
            </Form.Item>
            <Form.Item
                name="phoneNumber"
                label="Номер телефону"
            >
                <Input placeholder="Номер телефону"/>
            </Form.Item>
            <Form.Item
                name="dateOfBirth"
                label="Дата народження"
            >
                <DatePicker/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonCreate loading={createStudentMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};

import React, {useCallback, useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {Navigate, useNavigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {AutoComplete, DatePicker, Form, Input, message} from 'antd';
import {ButtonUpdate} from '../../../../../../components/ButtonUpdate/ButtonUpdate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {UPDATE_USER_MUTATION, UpdateUserData, UpdateUserVars} from '../../users.mutations';
import {GET_USER_WITH_GRADE_QUERY, GetUserWithGradeData, GetUserWithGradeVars} from '../../users.queries';
import moment from 'moment';
import {Role} from '../../users.types';
import Search from 'antd/es/input/Search';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../../grades/grades.queries';
import debounce from 'lodash.debounce';
import Title from 'antd/es/typography/Title';
import CyrillicToTranslit from 'cyrillic-to-translit-js';
import {ukDateFormat} from '../../../../../../utils/formats';
import 'moment/locale/uk';

const cyrillicToTranslit = new CyrillicToTranslit({preset: 'uk'});


type FormValues = {
    id: string,
    firstName: string,
    lastName: string,
    middleName: string,
    login: string,
    email: string,
    phoneNumber: string,
    dateOfBirth: any,
    gradeName: string,
}

export const StudentsUpdate = () => {
    const params = useParams();
    const id = params.id as string;
    const [updateStudentMutation, updateStudentMutationOption] = useMutation<UpdateUserData, UpdateUserVars>(UPDATE_USER_MUTATION);
    const [form] = Form.useForm();
    const getStudentQuery = useQuery<GetUserWithGradeData, GetUserWithGradeVars>(GET_USER_WITH_GRADE_QUERY,
        {variables: {id: id}},
    );
    const [gradePage, setGradePage] = useState(1);
    const getGradeQuery = useQuery<GetGradesData, GetGradesVars>(GET_GRADES_QUERY, {
        variables: {
            page: gradePage,
            like: '',
        },
    });
    const navigate = useNavigate();



    const onFinish = async (values: FormValues) => {
        const gradeId = getStudentQuery.data?.getUser?.grade?.name === values.gradeName
            ? getStudentQuery.data?.getUser.gradeId
            : getGradeQuery.data?.getGrades.entities.find(grade => grade.name === values.gradeName)?.id;

        updateStudentMutation({
            variables: {
                updateUserInputType: {
                    id: values.id,
                    firstName: values.firstName,
                    lastName: values.lastName,
                    middleName: values.middleName,
                    login: values.login,
                    email: values.email,
                    phoneNumber: values.phoneNumber,
                    dateOfBirth: new Date(values.dateOfBirth._d.setHours(12)).toISOString(),
                    role: Role.Student,
                    gradeId: gradeId,
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

    const onSearchGradesHandler = async (value: string) => {
        const response = await getGradeQuery.refetch({
            page: 1,
            like: value,
        });
        if (!response.errors) {
            if (!response.data.getGrades.entities.length) {
                message.warning('Класів з даною назвою не знайдено');
            }
        } else {
            response.errors?.forEach(error => message.error(error.message));
        }
    };

    const debouncedSearchGradesHandler = useCallback(debounce(nextValue => onSearchGradesHandler(nextValue), 500), []);
    const searchGradesHandler = (value: string) => debouncedSearchGradesHandler(value);

    const changeLogin = () => {
        const lastName: string = form.getFieldValue('lastName') || '';
        const lastName1Letter = lastName.length ? lastName[0] : '';
        const firstName: string = form.getFieldValue('firstName') || '';
        const firstName1Letter = firstName.length ? firstName[0] : '';
        const middleName: string = form.getFieldValue('middleName') || '';
        const middleName1Letter = middleName.length ? middleName[0] : '';
        const dateOfBorn: Date | null = form.getFieldValue('dateOfBirth')?._d;
        const yearOfBorn = dateOfBorn?.getFullYear() || '';
        form.setFieldsValue({
            login: cyrillicToTranslit.transform(`${yearOfBorn}_${lastName1Letter}${firstName1Letter}${middleName1Letter}`).toLowerCase(),
        });
    };

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getStudentQuery.loading)
        return <Loading/>;

    const student = getStudentQuery.data?.getUser;
    return (
        <Form
            name="StudentsUpdateForm"
            onFinish={onFinish}
            form={form}
            initialValues={{
                id: student?.id,
                firstName: student?.firstName,
                middleName: student?.middleName,
                lastName: student?.lastName,
                login: student?.login,
                email: student?.email,
                phoneNumber: student?.phoneNumber,
                dateOfBirth: moment(student?.dateOfBirth.split('T')[0], 'YYYY-MM-DD'),
                gradeName: student?.grade?.name
            }}
            {...sizeFormItem}
        >
            <Title level={2}>Оновити учня</Title>
            <Form.Item name="id" style={{display: 'none'}}>
                <Input type={'hidden'}/>
            </Form.Item>
            <Form.Item
                name="lastName"
                label="Прізвище"
                rules={[{required: true, message: 'Введіть Прізвище!'}]}
            >
                <Input placeholder="Прізвище" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="firstName"
                label="Ім'я"
                rules={[{required: true, message: 'Введіть Ім\'я!'}]}
            >
                <Input placeholder="Ім'я" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="middleName"
                label="По-батькові"
                rules={[{required: true, message: 'Введіть По-батькові!'}]}
            >
                <Input placeholder="По-батькові" onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="login"
                label="Логін"
                rules={[{required: true, message: 'Введіть Логін!'}]}
            >
                <Input placeholder="Логін"/>
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
                <DatePicker format={ukDateFormat} onChange={() => changeLogin()}/>
            </Form.Item>
            <Form.Item
                name="gradeName"
                label="Клас"
            >
                <AutoComplete
                    options={getGradeQuery.data?.getGrades.entities.map(grade => ({value: grade.name}))}
                    onSearch={searchGradesHandler}
                >
                    <Search
                        placeholder="Клас"
                        enterButton
                        loading={getGradeQuery.loading}
                    />
                </AutoComplete>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonUpdate loading={updateStudentMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};

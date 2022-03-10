import React, {useCallback, useEffect, useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {AutoComplete, Form, Input, message, Tag} from 'antd';
import {useNavigate} from 'react-router-dom';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import 'moment/locale/uk';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {CREATE_SUBJECT_MUTATION, CreateSubjectData, CreateSubjectVars} from '../../subjects.mutations';
import Title from 'antd/es/typography/Title';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../../grades/grades.queries';
import debounce from 'lodash.debounce';
import Search from 'antd/es/input/Search';
import {Grade} from '../../../grades/grades.types';

type FormValues = {
    name: string,
}

export const SubjectsCreate = () => {
    const [createSubjectMutation, createSubjectMutationOption] = useMutation<CreateSubjectData, CreateSubjectVars>(CREATE_SUBJECT_MUTATION);
    const [gradePage, setGradePage] = useState(1);
    const [gradeLike, setGradeLike] = useState('');
    const getGradesQuery = useQuery<GetGradesData, GetGradesVars>(GET_GRADES_QUERY, {
        variables: {
            page: gradePage,
            like: gradeLike,
        },
    });
    const [form] = Form.useForm();
    const navigate = useNavigate();
    const [grades, setGrades] = useState<Grade[]>([]);

    useEffect(() => {
        getGradesQuery.refetch({
            page: gradePage,
            like: gradeLike,
        })
            .then(response => {
                if (!response.data.getGrades.entities.length) {
                    message.warning('Класів з даною назвою не знайдено');
                }
            })
            .catch(error => {
                message.error(error.message);
            });
    }, [gradePage, gradeLike]);

    const onFinish = async ({name}: FormValues) => {
        createSubjectMutation({
            variables: {
                createSubjectInputType: {
                    name: name,
                    gradesHaveAccessReadIds: grades.map(grade => grade.id),
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

    const debouncedSearchGradesHandler = useCallback(debounce(nextValue => setGradeLike(nextValue), 500), []);
    const searchGradesHandler = (value: string) => debouncedSearchGradesHandler(value);

    const selectGradeHandler = (value: string) => {
        const newGrade = getGradesQuery.data?.getGrades.entities.find(grade => grade.name === value) as Grade;
        setGrades([...grades, newGrade]);
    };

    const removeGradeHandler = (value: string) => {
        const newGrades = grades.filter(grade => grade.name !== value);
        setGrades(newGrades);
    };

    return (
        <Form
            name="SubjectsCreateForm"
            onFinish={onFinish}
            form={form}
            {...sizeFormItem}
        >
            <Title level={2}>Створити предмет</Title>
            <Form.Item
                name="name"
                label="Назва"
                rules={[{required: true, message: 'Введіть назву!'}]}
            >
                <Input placeholder="Назва"/>
            </Form.Item>
            <Form.Item
                label="Класи"
            >
                <AutoComplete
                    options={getGradesQuery.data?.getGrades.entities.map(grade => ({value: grade.name}))}
                    onSearch={searchGradesHandler}
                    onSelect={selectGradeHandler}
                >
                    <Search
                        placeholder="Класи"
                        enterButton
                        loading={getGradesQuery.loading}
                    />
                </AutoComplete>
                {grades.map(grade => (
                    <Tag
                        closable
                        onClose={e => {
                            e.preventDefault();
                            removeGradeHandler(grade.name);
                        }}
                    >{grade.name}</Tag>
                ))}
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonCreate loading={createSubjectMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
